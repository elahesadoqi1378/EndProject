using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Repository;
using Achareh.Domain.Core.Dtos.HomeService;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Achareh.Endpoint.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Threading;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeServiceController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly ISubCategoryAppService _subCategoryAppService;
       


        public HomeServiceController(IHomeServiceAppService homeServiceAppService, IWebHostEnvironment webHostEnvironment, ISubCategoryAppService subCategoryAppService)
        {
            _homeServiceAppService = homeServiceAppService;
            _subCategoryAppService = subCategoryAppService;
            _webHostEnvironment = webHostEnvironment;
            
        }

        public async Task<IActionResult> HomeServiceIndex(CancellationToken cancellationToken)
        {
            var homeServices = await _homeServiceAppService.GetAllAsync(cancellationToken);
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
        public async Task<IActionResult> Create(CreateHomeServiceViewModel model, IFormFile ImageFile, CancellationToken cancellationToken)
        {
            model.ImagePath = model.ImagePath ?? string.Empty;
            if (!ModelState.IsValid)
            {
                ViewBag.SubCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
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
                ViewBag.SubCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
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

            var subCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
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
                ViewBag.Subcategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
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
                ViewBag.subCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
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
