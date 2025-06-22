using Atsumaru.Data;
using Atsumaru.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims; // Cần thiết để truy cập Claims
using Microsoft.AspNetCore.Authorization; // Cần thiết cho thuộc tính [Authorize]

namespace Atsumaru.Controllers
{
    // Áp dụng [Authorize] cho toàn bộ controller hoặc chỉ cho các action cần thiết.
    // Nếu bạn muốn người dùng phải đăng nhập để truy cập bất kỳ chức năng nào của ContactController,
    // hãy đặt [Authorize] ở đây.
    // [Authorize]
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
            
            // bạn có thể lấy nó ở đây và gán vào model.
            var model = new Contact();
            if (User.Identity.IsAuthenticated)
            {
                
                model.Name = User.Identity.Name;
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Chỉ người dùng đã xác thực mới có thể gửi tin nhắn
        public async Task<IActionResult> Send(Contact model)
        {
           
            // đã thêm email vào Claims của người dùng khi họ đăng nhập.
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                
                // nhưng là một biện pháp bảo vệ tốt.
                ViewBag.ErrorMessage = "Không thể xác định email của bạn. Vui lòng đăng nhập lại.";
                ModelState.Clear(); // Xóa trạng thái của model để form được reset
                return View("Index", new Contact());
            }

            
            ModelState.Remove("Email");

            if (ModelState.IsValid)
            {
                var contactMessage = new Contact
                {
                    Name = model.Name, // Lấy tên từ form người dùng nhập
                    Email = userEmail, // Tự động gán email của người dùng đang đăng nhập
                    Message = model.Message,
                    SentDate = DateTime.Now,
                    AdminReply = null, // Đảm bảo null cho tin nhắn mới
                    ReplyDate = null
                };

                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                ViewBag.SuccessMessage = "Tin nhắn của bạn đã được gửi thành công. Chúng tôi sẽ liên hệ lại với bạn sớm nhất!";

                ModelState.Clear(); // Xóa trạng thái của model để form được reset
                return View("Index", new Contact()); // Quay lại trang Index với form trống
            }

            ViewBag.ErrorMessage = "Vui lòng kiểm tra lại thông tin bạn đã nhập.";
            return View("Index", model); // Quay lại trang Index với lỗi validation
        }

        [Authorize] // Chỉ người dùng đã xác thực mới có thể xem tin nhắn của họ
        public async Task<IActionResult> MyMessages()
        {
       
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {

                // hiển thị thông báo lỗi.
                ViewBag.NoMessagesMessage = "Không thể tìm thấy email người dùng. Vui lòng đăng nhập lại.";
                return View(new List<Contact>()); // Trả về danh sách rỗng
            }

  
            var userContacts = await _context.ContactMessages
                                             .Where(c => c.Email.ToLower() == userEmail.ToLower())
                                             .OrderByDescending(c => c.SentDate)
                                             .ToListAsync();

            if (!userContacts.Any())
            {

                ViewBag.NoMessagesMessage = "Bạn chưa gửi tin nhắn nào. Hãy gửi tin nhắn đầu tiên của bạn!";
            }


            return View(userContacts);
        }
    }
}