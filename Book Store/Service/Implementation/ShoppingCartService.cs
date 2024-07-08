using Book_Store.Models.DTO;
using Book_Store.Models.Models;
using Book_Store.Repository.Interface;
using Book_Store.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Book> _BookRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BooksInOrder> _BookInOrderRepository;
     

        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Book> BookRepository, IRepository<Order> orderRepository, IRepository<BooksInOrder> BookInOrderRepository)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _BookRepository = BookRepository;
            _orderRepository = orderRepository;
            _BookInOrderRepository = BookInOrderRepository;
         
        }

        public ShoppingCart AddBookToShoppingCart(string userId, AddToCartDTO model)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var ShoppingCart = loggedInUser?.ShoppingCart;

                var selectedBook = _BookRepository.Get(model.SelectedBookId);

                if (selectedBook != null && ShoppingCart != null)
                {
                    ShoppingCart?.Books?.Add(new BooksInShoppingCart
                    {
                        Book = selectedBook,
                        BookId = selectedBook.Id,
                        ShoppingCart = ShoppingCart,
                        ShoppingCartId = ShoppingCart.Id,
                        Quantity = model.Quantity
                    });

                    return _shoppingCartRepository.Update(ShoppingCart);
                }
            }
            return null;
        }

        public bool deleteFromShoppingCart(string userId, int? Id)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);


                var Book_to_delete = loggedInUser?.ShoppingCart?.Books.First(z => z.Id == Id);

                loggedInUser?.ShoppingCart?.Books?.Remove(Book_to_delete);

                _shoppingCartRepository.Update(loggedInUser.ShoppingCart);

                return true;

            }

            return false;
        }

        public AddToCartDTO getBookInfo(int Id)
        {
            var selectedBook = _BookRepository.Get(Id);
            if (selectedBook != null)
            {
                var model = new AddToCartDTO
                {
                    SelectedBookName = selectedBook.Title,
                    SelectedBookId = selectedBook.Id,
                    Quantity = 1
                };
                return model;
            }
            return null;
        }

        public ShoppingCartDTO getShoppingCartDetails(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);

                var allBooks = loggedInUser?.ShoppingCart?.Books?.ToList();

                var totalPrice = 0.0;

                foreach (var item in allBooks)
                {
                    totalPrice += Double.Round((item.Quantity * item.Book.Price), 2);
                }

                var model = new ShoppingCartDTO
                {
                    AllBooks = allBooks,
                    TotalPrice = totalPrice
                };

                return model;

            }

            return new ShoppingCartDTO
            {
                AllBooks = new List<BooksInShoppingCart>(),
                TotalPrice = 0.0
            };
        }

        public bool orderBooks(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.ShoppingCart;


                var userOrder = new Order
                {
                    
                    BookUserId = userId,
                    BookUser = loggedInUser
                };

                _orderRepository.Insert(userOrder);

                var booksInOrders = userCart?.Books.Select(z => new BooksInOrder
                {
                    Order = userOrder,
                    OrderId = userOrder.Id,
                    BookId = z.BookId,
                    Book = z.Book,
                    Quantity = z.Quantity
                }).ToList();

                var totalPrice = 0.0;

                for (int i = 1; i <= booksInOrders.Count(); i++)
                {
                    var currentItem = booksInOrders[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Book.Price;
                    loggedInUser.Orders.Add(currentItem);


                }
               
             
                _BookInOrderRepository.InsertMany(booksInOrders);

                userCart?.Books.Clear();

                _shoppingCartRepository.Update(userCart);

          

                return true;
            }
            return false;
        }

    }

}

