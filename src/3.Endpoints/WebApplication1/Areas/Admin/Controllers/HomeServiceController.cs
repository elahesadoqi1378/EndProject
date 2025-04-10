using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Achareh.Endpoint.MVC.Models;
using AChareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Threading;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeServiceController : Controller
    {
        
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly ISubCategoryAppService _subCategoryAppService;
        private readonly IHomeServiceDapperAppService _homeServiceDapperApp;
        private readonly ISubCategoryDapperAppService _subCategoryDapperAppService;
        private readonly IImageService _imageService;
       


        public HomeServiceController(IHomeServiceAppService homeServiceAppService, ISubCategoryAppService subCategoryAppService, IImageService imageService, IHomeServiceDapperAppService homeServiceDapperApp, ISubCategoryDapperAppService subCategoryDapperAppService)
        {
            _homeServiceAppService = homeServiceAppService;
            _subCategoryAppService = subCategoryAppService;
            _imageService = imageService;
            _homeServiceDapperApp = homeServiceDapperApp;
            _subCategoryDapperAppService = subCategoryDapperAppService;
            
            
        }

        public async Task<IActionResult> HomeServiceIndex(CancellationToken cancellationToken)
        {
            var homeServices = await _homeServiceDapperApp.GetAllAsync(cancellationToken);
            return View(homeServices);
        }
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryAppService.GetAllSubCategoriesAsync(cancellationToken);
            ViewBag.SubCategories = subCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHomeServiceViewModel model, CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                ViewBag.SubCategories = await _subCategoryDapperAppService.GetAllAsync(cancellationToken);
                return View(model);
            }
            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "homeservice", cancellationToken);
            }
            var newHomeService = new HomeService
            {
                Title = model.Title,
                SubCategoryId = model.SubCategoryId,
                ImagePath = model.ImagePath,
                Price = model.Price,
                Description = model.Description
            };

            var result = await _homeServiceAppService.CreateAsync(newHomeService, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "مشکلی در ایجاد زیردسته رخ داده است.");
                ViewBag.SubCategories = await _subCategoryDapperAppService.GetAllAsync(cancellationToken);
                return View(model);
            }

            return RedirectToAction("HomeServiceIndex");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var homeService = await _homeServiceAppService.GetByIdAsync(id, cancellationToken);
            if (homeService == null)
                return NotFound();

            var subCategories = await _subCategoryDapperAppService.GetAllAsync(cancellationToken);
            ViewBag.SubCategories = subCategories.Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.Title
            }).ToList();

            var model = new EditHomeServiceViewModel
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
        public async Task<IActionResult> Edit(EditHomeServiceViewModel model ,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Subcategories = await _subCategoryDapperAppService.GetAllAsync(cancellationToken);
                return View(model);
            }

            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "homeservice", cancellationToken);
            }
            var updatedHomeService = new HomeService
            {
                Id = model.Id,
                Title = model.Title,
                SubCategoryId = model.SubCategoryId,
                ImagePath = model.ImagePath ,
                Price = model.Price,
                Description = model.Description
               
            };

            var result = await _homeServiceAppService.UpdateAsync(updatedHomeService, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "مشکلی در به‌روزرسانی زیردسته رخ داده است.");
                ViewBag.subCategories = await _subCategoryDapperAppService.GetAllAsync(cancellationToken);
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
                ModelState.AddModelError("", "something is wrong in delete homeservice or maybe your subcategory is depend on some subcategories");
                return View();
            }

            return RedirectToAction("HomeServiceIndex");
        }
    }
}
