using Atsumaru.Data;
using Atsumaru.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atsumaru.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminClassificationsController : Controller
    {
        private readonly AtsumaruContextDB _context;

        public AdminClassificationsController(AtsumaruContextDB context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(ClassificationType type = ClassificationType.Category)
        {
            var model = new ClassificationViewModel { CurrentType = type };

            if (type == ClassificationType.Category)
            {
                model.Categories = await _context.categories.ToListAsync();
            }
            else // ClassificationType.Type
            {
                model.Types = await _context.types.ToListAsync();
            }

            ViewData["CurrentType"] = type.ToString(); // Để dùng trong View
            return View(model);
        }


        public IActionResult Create(ClassificationType type)
        {
            ViewData["CurrentType"] = type.ToString();
            return View(new ClassificationViewModel { CurrentType = type });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CurrentType")] ClassificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CurrentType == ClassificationType.Category)
                {
                    var category = new Models.category { categoryname = model.Name };
                    _context.Add(category);
                }
                else 
                {
                    var itemType = new Models.type { typename = model.Name };
                    _context.Add(itemType);
                }
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"{model.CurrentType} đã được thêm thành công.";
                return RedirectToAction(nameof(Index), new { type = model.CurrentType });
            }
            ViewData["CurrentType"] = model.CurrentType.ToString();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id, ClassificationType type)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (type == ClassificationType.Category)
            {
                var category = await _context.categories.FindAsync(id);
                if (category == null) return NotFound();
                return View(new ClassificationViewModel { Id = category.categoryId, Name = category.categoryname, CurrentType = type });
            }
            else // ClassificationType.Type
            {
                var itemType = await _context.types.FindAsync(id);
                if (itemType == null) return NotFound();
                return View(new ClassificationViewModel { Id = itemType.typeId, Name = itemType.typename, CurrentType = type });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CurrentType")] ClassificationViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.CurrentType == ClassificationType.Category)
                    {
                        var category = new Models.category { categoryId = model.Id, categoryname = model.Name };
                        _context.Update(category);
                    }
                    else 
                    {
                        var itemType = new Models.type { typeId = model.Id, typename = model.Name };
                        _context.Update(itemType);
                    }
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"{model.CurrentType} đã được cập nhật thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassificationExists(model.Id, model.CurrentType))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { type = model.CurrentType });
            }
            ViewData["CurrentType"] = model.CurrentType.ToString();
            return View(model);
        }


        public async Task<IActionResult> Delete(int? id, ClassificationType type)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy ID để xóa.";
                return NotFound();
            }

            ClassificationViewModel viewModel = null;

            if (type == ClassificationType.Category)
            {
                var category = await _context.categories.FirstOrDefaultAsync(m => m.categoryId == id);
                if (category == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thể loại này.";
                    return NotFound();
                }
                viewModel = new ClassificationViewModel { Id = category.categoryId, Name = category.categoryname, CurrentType = type };
            }
            else // ClassificationType.Type
            {
                var itemType = await _context.types.FirstOrDefaultAsync(m => m.typeId == id);
                if (itemType == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy loại này.";
                    return NotFound();
                }
                viewModel = new ClassificationViewModel { Id = itemType.typeId, Name = itemType.typename, CurrentType = type };
            }

            ViewData["CurrentType"] = type.ToString(); // Pass current type to the view for display
            return View(viewModel);
        }

        // POST: Admin/AdminClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, ClassificationType currentType) // currentType từ form hidden
        {
            try
            {
                object entityToDelete = null;

                if (currentType == ClassificationType.Category)
                {
                    entityToDelete = await _context.categories.FindAsync(id);
                }
                else // ClassificationType.Type
                {
                    entityToDelete = await _context.types.FindAsync(id);
                }

                if (entityToDelete == null)
                {
                    TempData["ErrorMessage"] = $"Không tìm thấy {currentType} để xóa.";
                    return RedirectToAction(nameof(Index), new { type = currentType });
                }

                // Remove the entity based on its type
                if (currentType == ClassificationType.Category)
                {
                    _context.categories.Remove((Models.category)entityToDelete);
                }
                else
                {
                    _context.types.Remove((Models.type)entityToDelete);
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"{currentType} đã được xóa thành công.";
            }
            catch (DbUpdateException ex)
            {
                // This exception usually indicates a foreign key constraint violation
                // (e.g., trying to delete a category that has associated products).
                Console.WriteLine($"Error deleting {currentType} with ID {id}: {ex.Message}");
                // Log the full exception details (ex) in a real application for debugging.

                TempData["ErrorMessage"] = $"Không thể xóa {currentType} này vì có dữ liệu liên quan. Vui lòng xóa tất cả các mục liên quan trước.";
                // You might want to redirect back to the delete confirmation page,
                // or just to the index, depending on desired UX.
                return RedirectToAction(nameof(Index), new { type = currentType });
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                Console.WriteLine($"An unexpected error occurred during deletion: {ex.Message}");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi không mong muốn khi xóa. Vui lòng thử lại.";
                return RedirectToAction(nameof(Index), new { type = currentType });
            }

            return RedirectToAction(nameof(Index), new { type = currentType });
        }

        private bool ClassificationExists(int id, ClassificationType type)
        {
            if (type == ClassificationType.Category)
            {
                return _context.categories.Any(e => e.categoryId == id);
            }
            else
            {
                return _context.types.Any(e => e.typeId == id);
            }
        }
    }
}
