using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.User;
using Achareh.Endpoint.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        
        private readonly UserManager<User> _userManager;
        private readonly ICustomerAppService _customerAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IImageService _imageService;

        public CustomerController(UserManager<User> userManager, ICustomerAppService customerAppService, ICityAppService cityAppService, IImageService imageService)
        {
            _userManager = userManager;
            _customerAppService = customerAppService;
            _cityAppService = cityAppService;
            _imageService = imageService;
        }
        public async Task<IActionResult> CustomerIndex(CancellationToken cancellationToken)
        {
            var customers = await _customerAppService.GetAllAsync(cancellationToken);

            var users = new List<CustomerExpertViewModel>();

            foreach(var customer in customers)
            {
                users.Add(new CustomerExpertViewModel()
                {
                    Id = customer.Id,
                    Email = customer.User.Email,
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    ImagePath = customer.User.ImagePath
                });
            }
            return  View(users);
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
        public async Task<IActionResult> Create(CreateUserViewModel model, CancellationToken cancellationToken)
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
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "customer", cancellationToken);
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

            var result = await _customerAppService.RegisterAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", "something is wrong");
                }

             
                var cities = await _cityAppService.GetAllAsync(cancellationToken);
                ViewData["Cities"] = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList();

                return View(model);
            }

            var customer = new Customer { User = user };
            await _customerAppService.CreateAsync(customer, cancellationToken);

            return RedirectToAction("CustomerIndex");
        }

        public async Task<IActionResult> Edit(int id , CancellationToken cancellationToken)
        {
            var customer = await _customerAppService.GetrByIdAsync(id, cancellationToken);
            if (customer == null)
                return NotFound();

            var model = new EditUserViewModel
            {
                Id = customer.Id,
                Email = customer.User.Email,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                ImagePath = customer.User.ImagePath

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
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "customer", cancellationToken);
            }

            user.UserName = model.Email;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.ImagePath = model.ImagePath;

            var result = await _customerAppService.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", "Something is wrong ");
                }
                return View(model);
            }
            var customer = await _customerAppService.GetrByIdAsync(model.Id, cancellationToken);
            if (customer == null)
                return NotFound();

            customer.User = user;
            await _customerAppService.UpdateAsync(customer, cancellationToken);
            return RedirectToAction("CustomerIndex");

        }
        public async Task<IActionResult> Delete(int id , CancellationToken cancellationToken)
        {
            var customer = await _customerAppService.GetrByIdAsync(id, cancellationToken);
            if (customer == null)
                return NotFound();

            return View(customer);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var result = await _customerAppService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "something is wrong in delete.");
                return View();
            }

            return RedirectToAction("CustomerIndex");
        }
    }
}
