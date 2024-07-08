using Book_Store.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Book_Store.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
   

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;

        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            return View(_shoppingCartService.getShoppingCartDetails(userId ?? ""));
        }

        // GET: ShoppingCarts/Details/5

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> DeleteBookFromShoppingCart(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.deleteFromShoppingCart(userId, id);

            return RedirectToAction("Index", "ShoppingCarts");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.orderBooks(userId ?? "");

            return RedirectToAction("Index");
        }

       

        public IActionResult SuccessPayment()
        {
            return View();
        }

    }
}


