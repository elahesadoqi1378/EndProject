using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Dtos.Category;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _categoryAppService;
      

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
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
        public async Task<IActionResult> Create(CreateCategoryDto model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _categoryAppService.CategoryCreate(model, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "something is wrong in create.");
                return View(model);
            }

            return RedirectToAction("CategoryIndex");
        }

      
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var category = await _categoryAppService.GetByIdAsync(id, cancellationToken);
            if (category == null)
                return NotFound();

            var model = new UpdateCategoryDto
            {
                Id = category.Id,
                Title = category.Title
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryDto model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _categoryAppService.CategoryUpdate(model, cancellationToken);
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
                ModelState.AddModelError("", "something is wrong in delete.");
                return View();
            }

            return RedirectToAction("CategoryIndex");
        }
    }
}


