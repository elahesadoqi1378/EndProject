using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Dtos.HomeService;
using Achareh.Domain.Core.Dtos.SubCategory;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Endpoint.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {

        private readonly ICategoryAppService _categoryAppService;
        private readonly ISubCategoryAppService _subCategoryAppService;

        public SubCategoryController(ISubCategoryAppService subCategoryAppService, ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
            _subCategoryAppService = subCategoryAppService;
        }

        public async Task<IActionResult> SubCategoryIndex(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);

            ViewData["categories"] = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();

            var subCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);

            return View(subCategories);


        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);

            ViewBag.Categories = categories; 
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryViewModel model, IFormFile ImageFile, CancellationToken cancellationToken)
        {
            model.ImagePath = model.ImagePath ?? string.Empty;
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
                return View(model);
            }
            string filePath = null;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                model.ImagePath = $"/uploads/{fileName}"; 
            }
            var newSubCategory = new SubCategory
            {
                Title = model.Title,
                CategoryId = model.CategoryId,
                ImagePath = model.ImagePath 
            };

            var result = await _subCategoryAppService.CreateAsync(newSubCategory, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "مشکلی در ایجاد زیردسته رخ داده است.");
                ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
                return View(model);
            }

            return RedirectToAction("SubCategoryIndex");
        }


        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var subCategory = await _subCategoryAppService.GetByIdAsync(id, cancellationToken);
            if (subCategory == null)
                return NotFound();

            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
            ViewBag.Categories = categories;

            var model = new EditSubCategoryViewModel
            {
                Id = subCategory.Id,
                Title = subCategory.Title,
                CategoryId = subCategory.CategoryId,
                ImagePath = subCategory.ImagePath
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(EditSubCategoryViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
                return View(model);
            }

            // بررسی و ذخیره تصویر جدید (در صورت آپلود)
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                model.ImagePath = $"/uploads/{fileName}"; // مسیر تصویر جدید
            }

            var updatedSubCategory = new SubCategory
            {
                Id = model.Id,
                Title = model.Title,
                CategoryId = model.CategoryId,
                ImagePath = model.ImagePath // مسیر جدید یا قبلی
            };

            var result = await _subCategoryAppService.UpdateAsync(updatedSubCategory, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "مشکلی در به‌روزرسانی زیردسته رخ داده است.");
                ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
                return View(model);
            }

            return RedirectToAction("SubCategoryIndex");
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var subcategory = await _subCategoryAppService.GetByIdAsync(id, cancellationToken);
            if (subcategory == null)
                return NotFound();

            return View(subcategory);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var result = await _subCategoryAppService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "something is wrong in delete.");
                return View();
            }

            return RedirectToAction("SubCategoryIndex");
        }



    }
}

