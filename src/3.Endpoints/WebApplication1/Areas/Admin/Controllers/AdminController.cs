using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminAppService _adminAppService;

        public AdminController(IAdminAppService adminAppService)
        {
            _adminAppService = adminAppService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
            
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _adminAppService.LoginAsync(email, password);
            if (!result.Succeeded)
            {

                ModelState.AddModelError("", "something is wrong in email or password");
                return View();
            }

            return RedirectToAction("Index", "Admin");
        }

        
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _adminAppService.LogoutAsync();
            return RedirectToAction("Login", "Admin");
        }

    }
}
