using Atsumaru.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atsumaru.Controllers
{
    public class ProductsController : Controller
    {
        private AtsumaruContextDB contextDB;
        public ProductsController(AtsumaruContextDB contextDB)
        {
            this.contextDB = contextDB;
        }
        public IActionResult ProductsByType(int typeId)
        {
            var products = contextDB.Products
                .Include(p => p.type)
                .Where(p => p.typeId == typeId)
            .ToList();

            var type = contextDB.types.FirstOrDefault(t => t.typeId == typeId);
            ViewBag.TypeName = type?.typename ?? "Không xác định";

            return View("ProductsByType", products);
        }
        public IActionResult ProductsByCategory(int cateId)
        {
            var products = contextDB.Products
                .Include(p => p.category)
                .Where(p => p.categoryId == cateId)
            .ToList();

            var type = contextDB.categories.FirstOrDefault(t => t.categoryId == cateId);
            ViewBag.categoryname = type?.categoryname ?? "Không xác định";

            return View("ProductsByCategory", products);
        }
    }
}
