using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Atsumaru.Data;
using Atsumaru.Models.ViewModel;
using Atsumaru.Models;
using System.Collections.Generic; // Ensure this is here for List<Product>

namespace Atsumaru.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AtsumaruContextDB _context;

        public ProfileController(
            UserManager<IdentityUser> userManager,
            AtsumaruContextDB context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Không thể tải thông tin người dùng.");
            }

            // Get userId once before the query
            var userId = user.Id;

            var favoriteProducts = await _context.WishlistItems
                                                 .Where(wi => wi.UserId == userId) // Use the pre-fetched userId
                                                 .Include(wi => wi.Product)
                                                 .Select(wi => wi.Product)
                                                 .ToListAsync();

            var model = new UserProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FavoriteProducts = favoriteProducts,
                ChangePasswordForm = new ChangePasswordViewModel() // Initialize the nested model
            };

            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View(model);
        }

        // GET: Profile/ChangePassword - This action might not be needed if you embed the form on Index
        // If you keep it, it should return a view that uses ChangePasswordViewModel directly
        [HttpGet]
        public IActionResult ChangePassword()
        {
            // If this is a standalone page, simply return its specific ViewModel
            return View(new ChangePasswordViewModel());
        }


        // POST: Profile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng.";
                return RedirectToAction(nameof(Index));
            }

            // Get userId once before any conditional logic to ensure it's available
            var userId = user.Id;

            if (!ModelState.IsValid)
            {
                // Repopulate the main UserProfileViewModel to send back to the Index view
                var userProfileModel = new UserProfileViewModel
                {
                    Id = userId,
                    UserName = user.UserName,
                    Email = user.Email,
                    FavoriteProducts = await _context.WishlistItems
                                                        .Where(wi => wi.UserId == userId) // Use pre-fetched userId
                                                        .Include(wi => wi.Product)
                                                        .Select(wi => wi.Product)
                                                        .ToListAsync(),
                    ChangePasswordForm = model // Pass the invalid ChangePasswordViewModel back
                };
                TempData["ErrorMessage"] = "Thay đổi mật khẩu không thành công. Vui lòng kiểm tra lại thông tin.";
                return View("Index", userProfileModel); // Return to Index view with errors
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                // If rendering on Index, pass model back to Index view to display validation errors
                var userProfileModel = new UserProfileViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FavoriteProducts = await _context.WishlistItems
                                                        .Where(wi => wi.UserId == user.Id)
                                                        .Include(wi => wi.Product)
                                                        .Select(wi => wi.Product)
                                                        .ToListAsync(),
                    ChangePasswordForm = model // Pass the invalid ChangePasswordViewModel back
                };
                TempData["ErrorMessage"] = "Thay đổi mật khẩu không thành công.";
                return View("Index", userProfileModel); // Return to Index view with errors
            }

            await _userManager.UpdateSecurityStampAsync(user); // Important for logging out all other sessions

            TempData["SuccessMessage"] = "Mật khẩu của bạn đã được thay đổi thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}