using Ecommerce.Data;
using Ecommerce.SessionHelper;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly ApplicationDbContext _db;
        public ShoppingCartController(ShoppingCart shoppingCart, ApplicationDbContext db)
        {
            _shoppingCart = shoppingCart;
            _db = db;
        }
        public IActionResult Index()
        {
            List<Cart> CartItems = new List<Cart>();
            if (HttpContext.Session.GetObjInSession<Cart>("cart") != null)
            {
                  CartItems = HttpContext.Session.GetObjInSession<Cart>("cart");

            }
                return View(CartItems);
        }
        public IActionResult AddToCart(int prId,int? qty)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == prId);
            _shoppingCart.AddToCart(product, qty ?? 1);
            HttpContext.Session.SetObjInSession("cart", _shoppingCart.CartItems);
            return RedirectToAction("Index","Home");
        }
    }
}
