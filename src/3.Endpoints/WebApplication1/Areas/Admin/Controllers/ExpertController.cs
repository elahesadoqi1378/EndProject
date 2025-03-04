using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.User;
using Achareh.Endpoint.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExpertController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IExpertAppService _expertAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IImageService _imageService;


        public ExpertController(UserManager<User> userManager, IExpertAppService expertAppService, ICityAppService cityAppService, IImageService imageService)
        {
            _userManager = userManager;
            _expertAppService = expertAppService;
            _cityAppService = cityAppService;
            _imageService = imageService;

        }
        public async Task<IActionResult> ExpertIndex(CancellationToken cancellationToken)
        {
            var experts = await _expertAppService.GetAllAsync(cancellationToken);
            var users = new List<CustomerExpertViewModel>();

            foreach (var expert in experts)
            {
                users.Add(new CustomerExpertViewModel
                {
                    Id = expert.Id,
                    Email = expert.User.Email,
                    FirstName = expert.User.FirstName,
                    LastName = expert.User.LastName,
                    ImagePath = expert.User.ImagePath

                });
            }
            return View(users);
        }
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllAsync(cancellationToken);
            ViewData["Cities"] = cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title

            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model , CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {

                var cities = await _cityAppService.GetAllAsync(cancellationToken);
                ViewData["Cities"] = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList();

                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "password and its repeated is not true");


                var cities = await _cityAppService.GetAllAsync(cancellationToken);
                ViewData["Cities"] = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList();

                return View(model);
            }

            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "expert", cancellationToken);
            }
            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                CityId = model.CityId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImagePath = model.ImagePath
            };

            var result = await _expertAppService.RegisterAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", "Something is wrong");
                }


                var cities = await _cityAppService.GetAllAsync(cancellationToken);
                ViewData["Cities"] = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList();

                return View(model);
            }

            var expert = new Expert { User = user };
            await _expertAppService.CreateAsync(expert, cancellationToken);

            return RedirectToAction("ExpertIndex");

        }
        public async Task<IActionResult> Edit(int id , CancellationToken cancellationToken)
        {
            var expert = await _expertAppService.GetByIdAsync(id, cancellationToken);
            if (expert == null)
                return NotFound();

            var model = new EditUserViewModel
            {
                Id = expert.Id,
                Email = expert.User.Email,
                FirstName = expert.User.FirstName,
                LastName = expert.User.LastName,
                ImagePath = expert.User.ImagePath
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model , CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound();

            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "expert", cancellationToken);
            }

            user.Email = model.Email;
            user.UserName = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.ImagePath = model.ImagePath;

            var result = await _expertAppService.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", "something is wrong");
                }
                return View(model);
            }
            var expert = await _expertAppService.GetByIdAsync(model.Id, cancellationToken);
            if (expert == null)
                return NotFound();


            expert.User.ImagePath = model.ImagePath;
            expert.User = user;

            await _expertAppService.UpdateAsync(expert, cancellationToken);

            return RedirectToAction("ExpertIndex");
        }
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var expert = await _expertAppService.GetByIdAsync(id, cancellationToken);
            if (expert == null)
                return NotFound();

            return View(expert);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var result = await _expertAppService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "something is wrong in delete.");
                return View();
            }

            return RedirectToAction("ExpertIndex");
        }


    }
}
