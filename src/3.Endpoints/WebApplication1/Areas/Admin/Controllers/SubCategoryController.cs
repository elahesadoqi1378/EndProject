using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Dtos.HomeService;
using Achareh.Domain.Core.Dtos.SubCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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

            ViewBag.Categories = categories; // ارسال لیست به ویو
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreasteSubCategoryDto model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken); // ارسال مجدد لیست
                return View(model);
            }

            var result = await _subCategoryAppService.SubCategoryCreate(model, cancellationToken);
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

            ViewBag.Categories = categories; // ارسال لیست دسته‌بندی‌ها

            var model = new UpdateSubCategoryDto
            {
                Id = subCategory.Id,
                Title = subCategory.Title,
                CategoryId = subCategory.CategoryId,
                ImagePath = subCategory.ImagePath
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSubCategoryDto model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
                return View(model);
            }

            var result = await _subCategoryAppService.SubCategoryUpdate(model, cancellationToken);
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
            var subCategory = await _subCategoryAppService.GetByIdAsync(id, cancellationToken);
            if (subCategory == null)
                return NotFound();

            return View(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var result = await _subCategoryAppService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "something is wrong in delete Subcategory");
                return View();
            }

            return RedirectToAction("SubCategoryIndex");
        }
    }
}

