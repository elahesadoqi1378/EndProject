using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Endpoint.MVC.Models;
using AChareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {

        private readonly ICategoryAppService _categoryAppService;
        private readonly ISubCategoryAppService _subCategoryAppService;
        private readonly ISubCategoryDapperAppService _subCategoryDapperApp;
        private readonly IImageService _imageService;

        public SubCategoryController(ISubCategoryAppService subCategoryAppService, ICategoryAppService categoryAppService, IImageService imageService, ISubCategoryDapperAppService subCategoryDapperApp)
        {
            _categoryAppService = categoryAppService;
            _subCategoryAppService = subCategoryAppService;
            _imageService = imageService;
            _subCategoryDapperApp = subCategoryDapperApp;
        }

        public async Task<IActionResult> SubCategoryIndex(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);

            ViewData["categories"] = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();

            var subCategories = await _subCategoryDapperApp.GetAllAsync(cancellationToken);

            return View(subCategories);


        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);

            ViewBag.Categories = categories; 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryViewModel model, CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
                return View(model);
            }

            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "subcategory", cancellationToken);
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


            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "subcategory", cancellationToken);
            }

            var updatedSubCategory = new SubCategory
            {
                Id = model.Id,
                Title = model.Title,
                CategoryId = model.CategoryId,
                ImagePath = model.ImagePath 
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
                TempData["DeleteError"] = "مشکلی در حذف زیر دسته‌بندی وجود دارد یا این زیر دسته‌بندی دارای زیردسته‌های وابسته است.";
                return RedirectToAction("SubCategoryIndex");
               
            }

            TempData["Success"] = "با موفقیت حذف شد.";
            return RedirectToAction("SubCategoryIndex");
        }



    }
}

