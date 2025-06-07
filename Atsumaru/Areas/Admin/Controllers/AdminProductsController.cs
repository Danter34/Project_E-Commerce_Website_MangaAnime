using Atsumaru.Data;
using Atsumaru.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic; // Cần thiết để xử lý List<IFormFile>

namespace Atsumaru.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminProductsController : Controller
    {
        private readonly AtsumaruContextDB _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminProductsController(AtsumaruContextDB context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/AdminProducts (Hiển thị danh sách sản phẩm)
        public async Task<IActionResult> Index(string searchTerm)
        {
            IQueryable<Product> atsumaruContextDB = _context.Products
                .Include(p => p.category)
                .Include(p => p.type);

           
            if (!string.IsNullOrEmpty(searchTerm))
            {
                atsumaruContextDB = atsumaruContextDB.Where(p => p.Name.Contains(searchTerm));
                ViewBag.CurrentSearchTerm = searchTerm; 
            }

            return View(await atsumaruContextDB.ToListAsync());
        }

        // GET: Admin/AdminProducts/Details/5 (Xem chi tiết sản phẩm)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.category)
                .Include(p => p.type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/AdminProducts/Create (Hiển thị Form thêm sản phẩm mới)
        public IActionResult Create()
        {
            ViewData["categoryId"] = new SelectList(_context.categories, "categoryId", "categoryname");
            ViewData["typeId"] = new SelectList(_context.types, "typeId", "typename");
            return View();
        }

        // POST: Admin/AdminProducts/Create (Xử lý lưu sản phẩm mới)
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Sửa Bind để bao gồm tất cả các Image properties
        public async Task<IActionResult> Create(
            [Bind("Id,Name,tag,Detail,Price,Banner,Hotupdate,top10,Newproduct,Lastupdate,categoryId,typeId")] Product product,
            IFormFile imageFile, IFormFile image1File, IFormFile image2File, IFormFile image3File, IFormFile image4File, IFormFile image5File)
        {
            if (ModelState.IsValid)
            {
                // Gọi hàm xử lý và lưu ảnh
                await SaveImage(imageFile, p => product.Image = p);
                await SaveImage(image1File, p => product.Image1 = p);
                await SaveImage(image2File, p => product.Image2 = p);
                await SaveImage(image3File, p => product.Image3 = p);
                await SaveImage(image4File, p => product.Image4 = p);
                await SaveImage(image5File, p => product.Image5 = p);

                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sản phẩm đã được thêm thành công.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoryId"] = new SelectList(_context.categories, "categoryId", "categoryname", product.categoryId);
            ViewData["typeId"] = new SelectList(_context.types, "typeId", "typename", product.typeId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Edit/5 (Hiển thị Form chỉnh sửa sản phẩm)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["categoryId"] = new SelectList(_context.categories, "categoryId", "categoryname", product.categoryId);
            ViewData["typeId"] = new SelectList(_context.types, "typeId", "typename", product.typeId);
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5 (Xử lý lưu thay đổi sản phẩm)
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Thêm các IFormFile cho từng ảnh
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Name,tag,Detail,Price,Banner,Hotupdate,top10,Newproduct,Lastupdate,Image,Image1,Image2,Image3,Image4,Image5,categoryId,typeId")] Product product,
            IFormFile? imageFile, IFormFile? image1File, IFormFile? image2File, IFormFile? image3File, IFormFile? image4File, IFormFile? image5File)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy thông tin sản phẩm hiện tại từ DB để xử lý xóa ảnh cũ
                    var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Xử lý từng ảnh một
                    await UpdateAndSaveImage(imageFile, product.Image, p => product.Image = p);
                    await UpdateAndSaveImage(image1File, product.Image1, p => product.Image1 = p);
                    await UpdateAndSaveImage(image2File, product.Image2, p => product.Image2 = p);
                    await UpdateAndSaveImage(image3File, product.Image3, p => product.Image3 = p);
                    await UpdateAndSaveImage(image4File, product.Image4, p => product.Image4 = p);
                    await UpdateAndSaveImage(image5File, product.Image5, p => product.Image5 = p);
                    
                    // Cập nhật sản phẩm vào database
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Sản phẩm đã được cập nhật thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoryId"] = new SelectList(_context.categories, "categoryId", "categoryname", product.categoryId);
            ViewData["typeId"] = new SelectList(_context.types, "typeId", "typename", product.typeId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Delete/5 (Hiển thị Form xác nhận xóa sản phẩm)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.category)
                .Include(p => p.type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/AdminProducts/Delete/5 (Xử lý xóa sản phẩm)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                // Xóa tất cả các file ảnh liên quan
                DeleteImageFile(product.Image);
                DeleteImageFile(product.Image1);
                DeleteImageFile(product.Image2);
                DeleteImageFile(product.Image3);
                DeleteImageFile(product.Image4);
                DeleteImageFile(product.Image5);

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sản phẩm đã được xóa thành công.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        // Helper method để lưu một file ảnh và cập nhật thuộc tính Image
        private async Task SaveImage(IFormFile? file, Action<string?> updateProperty)
        {
            if (file != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string uploadsFolder = Path.Combine(wwwRootPath, "images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                updateProperty("/images/" + fileName);
            }
            // Nếu file là null, không làm gì cả, thuộc tính Image sẽ giữ giá trị null hoặc rỗng
        }

        // Helper method để cập nhật file ảnh (xóa cũ, lưu mới)
        private async Task UpdateAndSaveImage(IFormFile? newFile, string? oldImagePath, Action<string?> updateProperty)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            if (newFile != null)
            {
                // Xóa ảnh cũ nếu nó tồn tại và không phải ảnh mặc định
                DeleteImageFile(oldImagePath);

                // Lưu ảnh mới
                await SaveImage(newFile, updateProperty);
            }
            // else: Nếu newFile là null, giữ nguyên oldImagePath (đã được bind từ hidden input)
        }

        // Helper method để xóa một file ảnh
        private void DeleteImageFile(string? imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string oldFilePath = Path.Combine(wwwRootPath, imagePath.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
        }
    }
}