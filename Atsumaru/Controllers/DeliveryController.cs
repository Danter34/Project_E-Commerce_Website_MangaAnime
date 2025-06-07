using Atsumaru.Models;
using Atsumaru.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Atsumaru.Data;
using Atsumaru.Models.Services; 

namespace Atsumaru.Controllers
{
    [Authorize]
    public class DeliveryController : Controller
    {
        private IDeliveryRepository DeliveryRepository;
        private ICartRepository CartRepository;
        private readonly AtsumaruContextDB _context; 

        public DeliveryController(IDeliveryRepository DeliveryRepository, ICartRepository CartRepossitory, AtsumaruContextDB context)
        {
            this.DeliveryRepository = DeliveryRepository;
            this.CartRepository = CartRepossitory;
            _context = context;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Delivery delivery)
        {
            var items = CartRepository.GetAllCartItems();
            if (items == null || !items.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống");
            }

            if (ModelState.IsValid && (items != null && items.Any()))
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if (userEmail != null)
                {
                    delivery.Email = userEmail;
                }
                delivery.DeliveryPlaced = DateTime.Now;
                delivery.Status = "Đang xử Lý"; 
                DeliveryRepository.PlaceDelivery(delivery);
                CartRepository.ClearCart();
                HttpContext.Session.SetInt32("CartCount", 0);

                return RedirectToAction("CheckoutComplete");
            }
            return View(delivery);
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }

        public async Task<IActionResult> MyOrders() 
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var userDeliveries = await _context.Deliverys 
                                                .Where(o => o.Email == userEmail)
                                                .Include(o => o.DeliveryDetails)
                                                    .ThenInclude(od => od.Product)
                                                .OrderByDescending(o => o.DeliveryPlaced)
                                                .ToListAsync();

            return View(userDeliveries);
        }


        public async Task<IActionResult> DeliveryDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }


            var delivery = await _context.Deliverys
                .Include(d => d.DeliveryDetails)
                    .ThenInclude(dd => dd.Product)
                .Where(d => d.Id == id && d.Email == userEmail) 
                .FirstOrDefaultAsync();

            if (delivery == null)
            {
                return NotFound(); 
            }

            return View(delivery);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelDelivery(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Unauthorized(); 
            }

            var delivery = await _context.Deliverys
                                        .Where(d => d.Id == id && d.Email == userEmail)
                                        .FirstOrDefaultAsync();

            if (delivery == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy đơn hàng hoặc bạn không có quyền hủy đơn hàng này.";
                return RedirectToAction(nameof(MyOrders));
            }

            // Chỉ cho phép hủy nếu trạng thái là "Pending"
            if (delivery.Status == "Đang xử Lý")
            {
                delivery.Status = "Đã Hủy";
                try
                {
                    _context.Update(delivery);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Đơn hàng đã được hủy thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi hủy đơn hàng. Vui lòng thử lại.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = $"Đơn hàng không thể hủy ở trạng thái '{delivery.Status}'.";
            }

            return RedirectToAction(nameof(MyOrders));
        }
    }
}