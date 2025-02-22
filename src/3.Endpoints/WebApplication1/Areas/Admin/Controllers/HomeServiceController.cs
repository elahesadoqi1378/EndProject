using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Dtos.HomeService;
using Achareh.Domain.Core.Entities.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeServiceController : Controller
    {
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly ISubCategoryAppService _subCategoryAppService;

        public HomeServiceController(IHomeServiceAppService homeServiceAppService, ISubCategoryAppService subCategoryAppService)
        {
            _homeServiceAppService = homeServiceAppService;
            _subCategoryAppService = subCategoryAppService;
        }

        public async Task<IActionResult> HomeServiceIndex(CancellationToken cancellationToken)
        {
            var homeServices = await _homeServiceAppService.GetAllAsync(cancellationToken);

            return View(homeServices);
        }
        public async Task<IActionResult> Details(int id , CancellationToken cancellationToken)
        {
            var homeService = _homeServiceAppService.GetByIdAsync(id, cancellationToken);
            if (homeService == null)
                return NotFound();

            return View(homeService);
        }
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);

            ViewData["SubCategories"] = subCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHomeServiceDto model , CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _homeServiceAppService.HomeServiceCreate(model, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "something is wrong in create new homeservice");
                return View(model);
            }

            return RedirectToAction("HomeServiceIndex");
        }
        public async Task<IActionResult> Edit(int id , CancellationToken cancellationToken)
        {
            var homeService = await _homeServiceAppService.GetByIdAsync(id, cancellationToken);
            if (homeService == null)
                return NotFound();

            var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);

            ViewData["SubCategories"] = subCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();

            var model = new UpdateHomeServiceDto
            {
                Id = homeService.Id,
                Title = homeService.Title,
                SubCategoryId = homeService.SubCategoryId,
                ImagePath = homeService.ImagePath,
                Price = homeService.Price,
                Description = homeService.Description
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateHomeServiceDto model ,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
               
                var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);
                ViewData["SubCategories"] = subCategories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList();

                return View(model);
            }

            var result = await _homeServiceAppService.HomeServiceUpdate(model, cancellationToken);

            if (!result)
            {
                ModelState.AddModelError("", "something is wrong in update homeservice");

                var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);
                ViewData["SubCategories"] = subCategories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList();

                return View(model);
            }

            return RedirectToAction("HomeServiceIndex");
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var homeService = await _homeServiceAppService.GetByIdAsync(id, cancellationToken);
            if (homeService == null)
                return NotFound();

            return View(homeService);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var result = await _homeServiceAppService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "something is wrong in delete homeservice");
                return View();
            }

            return RedirectToAction("HomeServiceIndex");
        }
    }
}
