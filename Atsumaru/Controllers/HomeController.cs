using Atsumaru.Models;
using Atsumaru.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Atsumaru.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepository;
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            ViewBag.HotUpdate = productRepository.GetHotupdate();
            ViewBag.Top10 = productRepository.Gettop10();
            ViewBag.NewProduct = productRepository.GetNewproduct();
            ViewBag.LastUpdate = productRepository.GetLastupdate();

            return View(productRepository.GetBanner());
        }
        public IActionResult Detail(int id)
        {
            var product = productRepository.GetProductDetail(id);
            if (product != null)
            {
                return View(product);
            }
            return NotFound();
        }
    }

}
