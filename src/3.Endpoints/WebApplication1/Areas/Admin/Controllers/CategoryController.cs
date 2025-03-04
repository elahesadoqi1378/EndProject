using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Dtos.Category;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Endpoint.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _categoryAppService;
        private readonly IImageService _imageService;
      

        public CategoryController(ICategoryAppService categoryAppService, IImageService imageService)
        {
            _categoryAppService = categoryAppService;
            _imageService = imageService;
        }

        
        public async Task<IActionResult> CategoryIndex(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllCategoriesAsync(cancellationToken);
            return View(categories);
        }

    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return View(model);
            

            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "category", cancellationToken);
            }

            var newCategory = new Category
            {
                Title = model.Title,
                ImagePath = model.ImagePath
            };

            var result = await _categoryAppService.CreateAsync(newCategory, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "مشکلی در ایجاد دسته رخ داده است.");
                return View(model);
            }

            return RedirectToAction("CategoryIndex");
        }

      
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var category = await _categoryAppService.GetByIdAsync(id, cancellationToken);
            if (category == null)
                return NotFound();

            var model = new UpdateCategoryViewModel
            {
                Id = category.Id,
                Title = category.Title,
                ImagePath = category.ImagePath
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "category", cancellationToken);
            }

            var updatedCategory = new Category
            {
                Id = model.Id,
                Title = model.Title,
                ImagePath = model.ImagePath
            };


            var result = await _categoryAppService.UpdateAsync(updatedCategory, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "something is wrong in edit.");
                return View(model);
            }

            return RedirectToAction("CategoryIndex");
        }

        
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var category = await _categoryAppService.GetByIdAsync(id, cancellationToken);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var result = await _categoryAppService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
               
                return RedirectToAction("CategoryIndex"); 
            }

            TempData["DeleteError"] = "مشکلی در حذف دسته‌بندی وجود دارد یا این دسته‌بندی دارای زیردسته‌های وابسته است.";
            return RedirectToAction("CategoryIndex");
        }
    }
}


