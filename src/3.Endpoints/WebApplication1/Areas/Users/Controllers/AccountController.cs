using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Entities.User;
using AChareh.Domain.Core.Contracts.AppService;
using AChareh.Domain.Core.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class AccountController : Controller
    {
        private readonly ICustomerExpertLoginAppService _customerExpertLoginAppService;
        private readonly UserManager<User> _userManager;
       

        public AccountController(ICustomerExpertLoginAppService customerExpertLoginAppService, UserManager<User> userManager)
        {
            _customerExpertLoginAppService = customerExpertLoginAppService;
            _userManager = userManager;
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
                    var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
                    if (await _userManager.IsInRoleAsync(user, "Customer") && loginUserDto.Role == "Expert" )  
                    {
                        ModelState.AddModelError("Role", "شما نمی‌توانید نقش کارشناس را انتخاب کنید.");
                        await _customerExpertLoginAppService.Logout(); 
                        return View(loginUserDto);
                    }

                    return RedirectToAction("Index", "CustomerDashboard");
                }

                ModelState.AddModelError("", " email or password is wrong");
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
