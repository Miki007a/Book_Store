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
        private readonly IShoppingCartService shoppingCartService;

        public BooksController(IPublisherService publisherService, IBookService bookService,IOrderService orderService,IShoppingCartService shoppingCartService)
        {
            this.publisherService = publisherService;
            this.bookService = bookService;
            this.orderService = orderService;

            this.shoppingCartService = shoppingCartService;
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

    
        public IActionResult AddBookToCart(int Id)
        {
            var result = shoppingCartService.getBookInfo(Id);
            if (result != null)
            {
                return View(result);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult AddBookToCart(AddToCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = shoppingCartService.AddBookToShoppingCart(userId, model);

            if (result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else { return View(model); }
        }



    }
}
