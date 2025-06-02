using Atsumaru.Models;
using Atsumaru.Models.Interface;
using Atsumaru.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace Atsumaru.Controllers
{
    public class CartController : Controller
    {
        private ICartRepository CartRepository;
        private IProductRepository productRepository;
        public CartController(ICartRepository CartRepository, IProductRepository productRepository)
        {
            this.CartRepository = CartRepository;
            this.productRepository = productRepository;

        }

        public IActionResult Index()
        {
            var items = CartRepository.GetAllCartItems();
            CartRepository.carts = items;
            ViewBag.TotalCart = CartRepository.GetCartTotal();
            return View(items);
        }

        public RedirectToActionResult AddCart(int pId)
        {
            var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id ==
pId);
            if (product != null)
            {
                CartRepository.AddCart(product);
                int cartCount = CartRepository.GetAllCartItems().Count();
                HttpContext.Session.SetInt32("CartCount", cartCount);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveCart(int pId)
        {
            var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id ==
pId);
            if (product != null)
            {
                CartRepository.RemoveCart(product);
                int cartCount = CartRepository.GetAllCartItems().Count();
                HttpContext.Session.SetInt32("CartCount", cartCount);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> IncreaseQty(string productId) 
        {
            var success = await CartRepository.IncreaseQuantity(productId);

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("lỗi");
            }
        }
        public async Task<IActionResult> DecreaseQty(string productId)
        {
            var success = await CartRepository.DecreaseQuantity(productId);
            int cartCount = CartRepository.GetAllCartItems().Count();
            HttpContext.Session.SetInt32("CartCount", cartCount);

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("lỗi");
            }
        }
    }
}
