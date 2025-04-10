using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Entities.User;
using AChareh.Domain.Core.Contracts.AppService;
using AChareh.Domain.Core.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class AccountController : Controller
    {
        private readonly ICustomerExpertLoginAppService _customerExpertLoginAppService;
        private readonly ICityAppService _cityAppService;
        private readonly UserManager<User> _userManager;
       


        public AccountController(ICustomerExpertLoginAppService customerExpertLoginAppService, UserManager<User> userManager, ICityAppService cityAppService)
        {
            _customerExpertLoginAppService = customerExpertLoginAppService;
            _userManager = userManager;
            _cityAppService = cityAppService;
        }

        [HttpGet]
        [Route("Users/Account/Register")]
        public async Task<IActionResult> Register(CancellationToken cancellationToken)
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
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerExpertLoginAppService.RegisterAsync(registerUserDto,cancellationToken);
                if (result.Succeeded)
                {
                    var cities = await _cityAppService.GetAllAsync(cancellationToken);

                    ViewData["Cities"] = cities.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Title
                    }).ToList();

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", " email or password is wrong or your confirm pass is not ok!");
                return View(registerUserDto);

            }

            return View(registerUserDto);
        }



        [HttpGet]
        [Route("Users/Account/Login")]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto,CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerExpertLoginAppService.Login(loginUserDto);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", " email or password is wrong or your role is wrong");
                return View(loginUserDto);

            }

            return View(loginUserDto);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _customerExpertLoginAppService.Logout();
            return RedirectToAction("Index", "Home");
        }

    }
}
