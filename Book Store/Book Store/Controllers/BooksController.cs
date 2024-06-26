using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book_Store.Models.Models;
using Book_Store.Repository;
using Book_Store.Service.Interface;
using Book_Store.Models.DTO;
using System.Security.Claims;
using Book_Store.Service.Implementation;
using static NuGet.Packaging.PackagingConstants;

namespace Book_Store.Controllers
{
    public class BooksController : Controller
    {
        private readonly IPublisherService publisherService;
        private readonly IBookService bookService;
        private readonly IOrderService orderService;

        public BooksController(IPublisherService publisherService, IBookService bookService,IOrderService orderService)
        {
            this.publisherService = publisherService;
            this.bookService = bookService;
            this.orderService = orderService;
        }  

      
        public  IActionResult Index()
        {
            return View(publisherService.GetPublishers());
        }

       
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult Order(int? id)
        {
            Book book = bookService.GetBookById(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
            List<Order> orders = orderService.GetOrdersForUser(userId);
       
            BookInOrderDTO order = new BookInOrderDTO
            {
                Book = book,
                BookId = book.Id,
                Quantity = 0
            };
         

            return View(order);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order([Bind("Quantity,BookId")] BookInOrderDTO bookInOrder)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            bookService.AddBookInOrder(bookInOrder, userId);

            return RedirectToAction("Index","Orders");

        }
        public IActionResult AddProductToCart(int Id)
        {
            var result = _shoppingCartService.getProductInfo(Id);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddProductToCart(AddToCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.AddProductToShoppingCart(userId, model);

            if (result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else { return View(model); }
        }



    }
}
