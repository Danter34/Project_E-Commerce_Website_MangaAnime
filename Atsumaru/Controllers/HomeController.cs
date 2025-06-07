using Atsumaru.Models;
using Atsumaru.Models.Interface;
using Atsumaru.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims; 
using Microsoft.AspNetCore.Identity; 
using System.Threading.Tasks; 

namespace Atsumaru.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository; 
        private readonly UserManager<IdentityUser> _userManager; 
        public HomeController(IProductRepository productRepository, UserManager<IdentityUser> userManager) 
        {
            _productRepository = productRepository;
            _userManager = userManager; 
        }

        public IActionResult Index()
        {
            ViewBag.HotUpdate = _productRepository.GetHotupdate();
            ViewBag.Top10 = _productRepository.Gettop10();
            ViewBag.NewProduct = _productRepository.GetNewproduct();
            ViewBag.LastUpdate = _productRepository.GetLastupdate();

            return View(_productRepository.GetBanner());
        }

        public async Task<IActionResult> Detail(int id) 
        {
            var product = _productRepository.GetProductDetail(id); 
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                IsInWishlist = false 
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User); // Lấy ID của người dùng hiện tại
                if (userId != null)
                {
                    
                    viewModel.IsInWishlist = await _productRepository.IsProductInWishlistAsync(product.Id, userId);
                }
            }

            return View(viewModel); 
        }


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


            bool isAdded = await _productRepository.ToggleWishlistAsync(productId, userId);

            return Json(new { success = true, isAdded = isAdded, message = isAdded ? "Đã thêm vào danh sách yêu thích!" : "Đã xóa khỏi danh sách yêu thích." });
        }
        public async Task<IActionResult> Wishlist()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
 
                TempData["Message"] = "Vui lòng đăng nhập để xem danh sách yêu thích của bạn.";
                return Redirect("/Identity/Account/Login");
            }


            var productsInWishlist = await _productRepository.GetProductsInWishlistAsync(userId);

            return View(productsInWishlist);
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            ViewBag.CurrentSearchTerm = searchTerm;

            var searchResults = await _productRepository.SearchProductsAsync(searchTerm);


            if (searchResults == null || !searchResults.Any())
            {
                ViewBag.NoResultsMessage = $"Không tìm thấy sản phẩm nào khớp với từ khóa '{searchTerm}'.";
            }

            return View(searchResults); 
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