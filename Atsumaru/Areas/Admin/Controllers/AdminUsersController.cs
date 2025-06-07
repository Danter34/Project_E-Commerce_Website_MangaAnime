using Atsumaru.Data;
// using Atsumaru.Models; // KHÔNG CẦN DÒNG NÀY NẾU BẠN CHỈ DÙNG IdentityUser VÀ KHÔNG CÓ LỚP ApplicationUser TÙY CHỈNH
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; // ĐẢM BẢO DÒNG NÀY CÓ
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Atsumaru.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    // THAY ApplicationUser BẰNG IdentityUser Ở ĐÂY
    public class AdminUsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager; // THAY ApplicationUser BẰNG IdentityUser
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminUsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) // THAY ApplicationUser BẰNG IdentityUser
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/AdminUsers
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // GET: Admin/AdminUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) { return NotFound(); }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) { return NotFound(); }
            ViewBag.UserRoles = await _userManager.GetRolesAsync(user);
            return View(user);
        }

        // GET: Admin/AdminUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) { return NotFound(); }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) { return NotFound(); }
            ViewBag.AllRoles = await _roleManager.Roles.ToListAsync();
            ViewBag.UserRoles = await _userManager.GetRolesAsync(user);
            return View(user);
        }

        // POST: Admin/AdminUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // THAY ApplicationUser BẰNG IdentityUser VÀ CHỈ BIND Id, UserName, Email, PhoneNumber
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,PhoneNumber")] IdentityUser user)
        {
            if (id != user.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                var userInDb = await _userManager.FindByIdAsync(id);
                if (userInDb == null) { return NotFound(); }

                userInDb.UserName = user.UserName;
                userInDb.Email = user.Email;
                userInDb.PhoneNumber = user.PhoneNumber;
                // KHÔNG CẬP NHẬT các trường tùy chỉnh khác

                var result = await _userManager.UpdateAsync(userInDb);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Thông tin người dùng đã được cập nhật thành công.";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewBag.AllRoles = await _roleManager.Roles.ToListAsync();
            ViewBag.UserRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(id));
            return View(user);
        }

        // CÁC ACTION AddRoleToUser, RemoveRoleFromUser, Delete, DeleteConfirmed KHÔNG THAY ĐỔI
        // (chỉ cần đảm bảo tham số user là IdentityUser)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId); // user ở đây là IdentityUser
            if (user == null) { TempData["ErrorMessage"] = "Người dùng không tồn tại."; return RedirectToAction(nameof(Index)); }
            if (!await _roleManager.RoleExistsAsync(roleName)) { TempData["ErrorMessage"] = "Vai trò không tồn tại."; return RedirectToAction(nameof(Edit), new { id = userId }); }
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded) { TempData["SuccessMessage"] = $"Đã thêm vai trò '{roleName}' cho người dùng '{user.UserName}'."; }
            else { TempData["ErrorMessage"] = "Không thể thêm vai trò: " + string.Join(", ", result.Errors.Select(e => e.Description)); }
            return RedirectToAction(nameof(Edit), new { id = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId); // user ở đây là IdentityUser
            if (user == null) { TempData["ErrorMessage"] = "Người dùng không tồn tại."; return RedirectToAction(nameof(Index)); }
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded) { TempData["SuccessMessage"] = $"Đã gỡ vai trò '{roleName}' khỏi người dùng '{user.UserName}'."; }
            else { TempData["ErrorMessage"] = "Không thể gỡ vai trò: " + string.Join(", ", result.Errors.Select(e => e.Description)); }
            return RedirectToAction(nameof(Edit), new { id = userId });
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id); // user ở đây là IdentityUser
            if (user == null) return NotFound();
            if (user.UserName == User.Identity.Name) { TempData["ErrorMessage"] = "Bạn không thể tự xóa tài khoản của chính mình."; return RedirectToAction(nameof(Index)); }
            if (await _userManager.IsInRoleAsync(user, "Admin")) { TempData["ErrorMessage"] = "Bạn không thể xóa tài khoản của Admin khác từ đây."; return RedirectToAction(nameof(Index)); }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id); // user ở đây là IdentityUser
            if (user == null) return NotFound();
            if (user.UserName == User.Identity.Name || await _userManager.IsInRoleAsync(user, "Admin")) { TempData["ErrorMessage"] = "Không thể xóa tài khoản này vì lý do bảo mật."; return RedirectToAction(nameof(Index)); }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) { TempData["SuccessMessage"] = "Người dùng đã được xóa thành công."; }
            else { TempData["ErrorMessage"] = "Không thể xóa người dùng: " + string.Join(", ", result.Errors.Select(e => e.Description)); }
            return RedirectToAction(nameof(Index));
        }
    }
}