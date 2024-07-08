using Book_Store.Models.DTO;
using Book_Store.Models.Models;
using Book_Store.Repository.Implementation;
using Book_Store.Repository.Interface;
using Book_Store.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<PublisherBooks> _publisherBooksRepository;
        public BookService(IRepository<Book> repository, IUserRepository userRepository, IRepository<Order> _orderRepository, IRepository<PublisherBooks> publisherBooksRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            this._orderRepository = _orderRepository;
            _publisherBooksRepository = publisherBooksRepository;
        }



        public Book CreateNewBook(Book book)
        {
  
            return _repository.Insert(book);

        }

        public Book DeleteBook(int id)
        {
            var book = _repository.Get(id);
           return _repository.Delete(book);
        }

        public Book GetBookById(int? id)
        {
            return _repository.Get(id);
        }

        public List<Book> GetBooks()
        {
            return _repository.GetAll().ToList();
        }

   

        public Book UpdateBook(Book book)
        {
           return _repository.Update(book);
        }
    }
}
