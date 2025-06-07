using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Atsumaru.Data;

namespace Atsumaru.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Chỉ cho phép Admin truy cập
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AtsumaruContextDB _context; 

        public DashboardController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AtsumaruContextDB context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Thống kê người dùng và vai trò
            ViewBag.TotalUsers = await _userManager.Users.CountAsync();
            ViewBag.TotalRoles = await _roleManager.Roles.CountAsync();

            var adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole != null)
            {
                ViewBag.AdminUsersCount = (await _userManager.GetUsersInRoleAsync("Admin")).Count;
            }
            else
            {
                ViewBag.AdminUsersCount = 0;
            }

            var userRole = await _roleManager.FindByNameAsync("User");
            if (userRole != null)
            {
                ViewBag.RegularUsersCount = (await _userManager.GetUsersInRoleAsync("User")).Count;
            }
            else
            {
                ViewBag.RegularUsersCount = 0;
            }

            // Thống kê sản phẩm và đơn hàng
            ViewBag.TotalProducts = await _context.Products.CountAsync();
            ViewBag.TotalDeliveries = await _context.Deliverys.CountAsync(); // Lấy tổng số đơn hàng



            return View();
        }
    }
}