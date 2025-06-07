using Atsumaru.Data;
using Atsumaru.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Atsumaru.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly AtsumaruContextDB _context;

        public AdminOrdersController(AtsumaruContextDB context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var orders = await _context.Deliverys.OrderByDescending(o => o.DeliveryPlaced).ToListAsync();
            return View(orders);
        }

        // GET: Admin/AdminOrders/Details/5
        // Hiển thị chi tiết một đơn hàng (chỉ để xem, không sửa)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Deliverys
                .Include(o => o.DeliveryDetails) // Bao gồm chi tiết đơn hàng
                    .ThenInclude(od => od.Product) // Bao gồm thông tin sản phẩm trong chi tiết
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/AdminOrders/Edit/5
        // Chỉ cho phép admin vào trang này để cập nhật trạng thái đơn hàng
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Deliverys.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Danh sách các trạng thái đơn hàng có thể có
            // Đảm bảo các trạng thái này phù hợp với logic của bạn
            ViewBag.OrderStatuses = new List<string> { "Đang xử Lý", "Đã Giao", "Đang giao", "Đã Hủy", "Hoàn Tiền" };
            return View(order);
        }

        // POST: Admin/AdminOrders/Edit/5
        // Xử lý việc cập nhật trạng thái đơn hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        // CHỈNH SỬA DÒNG NÀY: Thay OrderStatus thành Status
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status")] Delivery order) // Đổi OrderStatus thành Status
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy đơn hàng hiện tại từ DB và THEO DÕI nó
                    var existingOrder = await _context.Deliverys.FirstOrDefaultAsync(o => o.Id == id);
                    if (existingOrder == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật DUY NHẤT trường Status
                    existingOrder.Status = order.Status; // order.Status giờ sẽ có giá trị đúng từ form

                    // EF Core sẽ tự động phát hiện thay đổi và cập nhật khi SaveChangesAsync() được gọi
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Trạng thái đơn hàng đã được cập nhật thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Xử lý lỗi đồng thời (concurrency)
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // Nếu có lỗi validation, quay lại view với danh sách trạng thái
            ViewBag.OrderStatuses = new List<string> { "Đang xử Lý", "Đã Giao", "Đang giao", "Đã Hủy", "Hoàn Tiền" };
            return View(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Deliverys.Any(e => e.Id == id);
        }
    }
}