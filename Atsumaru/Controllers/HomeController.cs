using Atsumaru.Models;
using Atsumaru.Models.Interface;
using Atsumaru.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims; // Thêm dòng này để sử dụng User.FindFirstValue
using Microsoft.AspNetCore.Identity; // Thêm dòng này để sử dụng UserManager
using System.Threading.Tasks; // Thêm dòng này để sử dụng Task

namespace Atsumaru.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository; // Thay đổi thành readonly
        private readonly UserManager<IdentityUser> _userManager; // Thêm UserManager

        public HomeController(IProductRepository productRepository, UserManager<IdentityUser> userManager) // Thêm UserManager vào constructor
        {
            _productRepository = productRepository;
            _userManager = userManager; // Gán UserManager
        }

        public IActionResult Index()
        {
            ViewBag.HotUpdate = _productRepository.GetHotupdate();
            ViewBag.Top10 = _productRepository.Gettop10();
            ViewBag.NewProduct = _productRepository.GetNewproduct();
            ViewBag.LastUpdate = _productRepository.GetLastupdate();

            return View(_productRepository.GetBanner());
        }

        public async Task<IActionResult> Detail(int id) // Chuyển sang async Task<IActionResult>
        {
            var product = _productRepository.GetProductDetail(id); // Sử dụng phương thức đồng bộ
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                IsInWishlist = false // Mặc định là false
            };

            // Kiểm tra nếu người dùng đã đăng nhập để xác định sản phẩm có trong wishlist không
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User); // Lấy ID của người dùng hiện tại
                if (userId != null)
                {
                    // Sử dụng phương thức IsProductInWishlistAsync từ ProductRepository
                    viewModel.IsInWishlist = await _productRepository.IsProductInWishlistAsync(product.Id, userId);
                }
            }

            return View(viewModel); // Truyền ViewModel sang View
        }

        // --- Thêm Action cho AJAX để thêm/xóa sản phẩm vào Wishlist ---
        [HttpPost]
        public async Task<IActionResult> ToggleWishlist(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thêm sản phẩm vào danh sách yêu thích.", redirectToLogin = true });
            }

            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Json(new { success = false, message = "Không thể tìm thấy thông tin người dùng." });
            }

            // Gọi phương thức ToggleWishlistAsync từ ProductRepository
            bool isAdded = await _productRepository.ToggleWishlistAsync(productId, userId);

            return Json(new { success = true, isAdded = isAdded, message = isAdded ? "Đã thêm vào danh sách yêu thích!" : "Đã xóa khỏi danh sách yêu thích." });
        }
        public async Task<IActionResult> Wishlist()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                // Hoặc hiển thị một thông báo lỗi, hoặc một trang wishlist trống
                TempData["Message"] = "Vui lòng đăng nhập để xem danh sách yêu thích của bạn.";
                return Redirect("/Identity/Account/Login");
            }

            // Lấy danh sách sản phẩm từ repository
            var productsInWishlist = await _productRepository.GetProductsInWishlistAsync(userId);

            // Truyền danh sách sản phẩm tới View
            return View(productsInWishlist);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}