using Atsumaru.Models;
using Atsumaru.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Atsumaru.Controllers
{
    public class DeliveryController : Controller
    {
        private IDeliveryRepository DeliveryRepository;
        private ICartRepository CartRepository;
        public DeliveryController(IDeliveryRepository DeliveryRepository,ICartRepository CartRepossitory)
        {
            this.DeliveryRepository = DeliveryRepository;
            this.CartRepository = CartRepossitory;
        }

        public IActionResult Checkout()  
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Delivery delivery)
        {
            DeliveryRepository.PlaceDelivery(delivery);
            CartRepository.ClearCart();
            HttpContext.Session.SetInt32("CartCount", 0);

            return RedirectToAction("CheckoutComplete");
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}
