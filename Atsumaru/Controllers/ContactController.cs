using Atsumaru.Data;
using Atsumaru.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this for .Where and .ToListAsync

namespace Atsumaru.Controllers
{
    public class ContactController : Controller
    {
        private readonly AtsumaruContextDB _context;

        public ContactController(AtsumaruContextDB context)
        {
            _context = context;
        }

        // GET: Hiển thị form liên hệ ban đầu
        public IActionResult Index()
        {
            // Truyền một đối tượng Contact trống để hiển thị form
            return View(new Contact());
        }

        // POST: Xử lý gửi tin nhắn mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(Contact model)
        {
            if (ModelState.IsValid)
            {
                var contactMessage = new Contact
                {
                    Name = model.Name,
                    Email = model.Email,
                    Message = model.Message,
                    SentDate = DateTime.Now,
                    AdminReply = null, // Đảm bảo các trường này là null khi tạo tin nhắn mới
                    ReplyDate = null
                };

                _context.ContactMessages.Add(contactMessage); // Đã sửa từ ContactMessages sang Contacts
                await _context.SaveChangesAsync();

                ViewBag.SuccessMessage = "Tin nhắn của bạn đã được gửi thành công. Chúng tôi sẽ liên hệ lại với bạn sớm nhất!";

                ModelState.Clear();
                return View("Index", new Contact()); // Quay lại trang Index với form trống
            }

            ViewBag.ErrorMessage = "Vui lòng kiểm tra lại thông tin bạn đã nhập.";
            return View("Index", model); // Quay lại trang Index với lỗi
        }

        // GET: Hiển thị trang để người dùng nhập email để tìm tin nhắn của họ
        public IActionResult MyMessages()
        {
            return View(); // Trả về một View trống để nhập email
        }

        // POST: Xử lý tìm kiếm tin nhắn dựa trên email và hiển thị phản hồi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyMessages(string emailToSearch)
        {
            if (string.IsNullOrWhiteSpace(emailToSearch))
            {
                ModelState.AddModelError("emailToSearch", "Vui lòng nhập địa chỉ email của bạn.");
                return View();
            }

            // Tìm tất cả tin nhắn liên quan đến email này
            var userContacts = await _context.ContactMessages
                                             .Where(c => c.Email == emailToSearch)
                                             .OrderByDescending(c => c.SentDate)
                                             .ToListAsync();

            if (!userContacts.Any())
            {
                ViewBag.NoMessagesMessage = "Không tìm thấy tin nhắn nào với địa chỉ email này. Vui lòng kiểm tra lại email hoặc gửi tin nhắn mới.";
            }

            // Trả về view với danh sách tin nhắn tìm được
            return View(userContacts);
        }

        // Bạn có thể giữ hoặc bỏ Confirmation() nếu bạn không dùng nó
        // public IActionResult Confirmation()
        // {
        //     return View();
        // }
    }
}