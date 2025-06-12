using Atsumaru.Data;
using Atsumaru.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Atsumaru.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Đảm bảo chỉ Admin mới truy cập được
    public class AdminContactsController : Controller
    {
        private readonly AtsumaruContextDB _context;

        public AdminContactsController(AtsumaruContextDB context)
        {
            _context = context;
        }

        // GET: Admin/AdminContacts (Hiển thị danh sách tin nhắn)
        public async Task<IActionResult> Index()
        {
            // Sắp xếp tin nhắn theo thời gian gửi giảm dần (tin mới nhất lên đầu)
            var contacts = await _context.ContactMessages.OrderByDescending(c => c.SentDate).ToListAsync();
            return View(contacts);
        }

        // GET: Admin/AdminContacts/Details/5 (Xem chi tiết tin nhắn và phản hồi)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Admin/AdminContacts/Reply/5 (Xử lý phản hồi và lưu vào DB)
        // Phương thức này sẽ được gọi khi admin gửi phản hồi từ trang Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply(int id, string adminReplyContent) // adminReplyContent là nội dung admin nhập vào
        {
            var contactToUpdate = await _context.ContactMessages.FindAsync(id);

            if (contactToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contactToUpdate.AdminReply = adminReplyContent; // Lưu nội dung phản hồi
                    contactToUpdate.ReplyDate = DateTime.Now;        // Ghi lại thời gian phản hồi

                    _context.Update(contactToUpdate);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Phản hồi đã được gửi thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contactToUpdate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = contactToUpdate.Id }); // Chuyển hướng lại trang chi tiết
            }
            // Nếu có lỗi, quay lại view Details với dữ liệu hiện tại
            return View("Details", contactToUpdate);
        }


        // GET: Admin/AdminContacts/Delete/5 (Hiển thị form xác nhận xóa)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Admin/AdminContacts/Delete/5 (Xử lý xóa tin nhắn)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.ContactMessages.FindAsync(id);
            if (contact != null)
            {
                _context.ContactMessages.Remove(contact);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Tin nhắn đã được xóa thành công.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.ContactMessages.Any(e => e.Id == id);
        }
    }
}