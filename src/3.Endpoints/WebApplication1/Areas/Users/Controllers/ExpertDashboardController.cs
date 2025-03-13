using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Achareh.Endpoint.MVC.Areas.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class ExpertDashboardController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IExpertAppService _expertAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IImageService _imageService;
        private readonly IHomeServiceAppService _homeServiceAppService;




        public ExpertDashboardController(UserManager<User> userManager, IExpertAppService expertAppService,ICityAppService cityAppService, IHomeServiceAppService homeServiceAppService)
        {
            _userManager = userManager;
            _expertAppService = expertAppService;
            _cityAppService = cityAppService;
            _homeServiceAppService = homeServiceAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (onlineUserId is null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(onlineUserId);

            var userInfo = await _expertAppService.GetExpertByIdWithDetailsAsync(userId, cancellationToken);

            return View(userInfo);

        }
       
        public async Task<IActionResult> EditExpertInfo(CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllAsync(cancellationToken);
            ViewBag.Cities = cities.Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.Title
            }).ToList();

            var homeServices = await _homeServiceAppService.GetAllAsync(cancellationToken);
            ViewBag.HomeServices = homeServices.Select(sh => new SelectListItem
            {
                Value = sh.Id.ToString(),
                Text = sh.Title
            }).ToList();

            var onlineUser = await _userManager.GetUserAsync(User);                                                                //////////////amaliyat haye tekrari baresi shavad


            if (onlineUser is null)
                return RedirectToAction("Login", "Account");

            var model = new EditExpertInfoViewModel
            {
                Id = onlineUser.Id,
                FirstName = onlineUser.FirstName,
                LastName = onlineUser.LastName,
                Email = onlineUser.Email,
                Address = onlineUser.Address,
                //ImagePath = onlineUser.ImagePath,
                PhoneNumber = onlineUser.PhoneNumber,
                CityId = onlineUser.CityId,
                UserName = onlineUser.UserName,
                Price = onlineUser.Inventory,
                Skills = onlineUser.Expert?.HomeServices?.Select(hs => hs.Id).ToList() ?? new List<int>()
            };

            return View(model);
        }

  

        [HttpPost]
        public async Task<IActionResult> EditExpertInfo(EditExpertInfoViewModel model, List<int> SelectedHomeServices, CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllAsync(cancellationToken);
            var homeServices = await _homeServiceAppService.GetAllAsync(cancellationToken);

            if (!ModelState.IsValid)
            {
                ViewBag.Cities = cities.Select(sc => new SelectListItem
                {
                    Value = sc.Id.ToString(),
                    Text = sc.Title
                }).ToList();

                ViewBag.HomeServices = homeServices.Select(sh => new SelectListItem
                {
                    Value = sh.Id.ToString(),
                    Text = sh.Title
                }).ToList();


                return View(model);
            }

            var onlineUser = await _userManager.GetUserAsync(User);
            if (onlineUser is null)
                return RedirectToAction("Login", "Account");

           

         
            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "expert", cancellationToken);
            }

            onlineUser.Id = model.Id; 
            onlineUser.FirstName = model.FirstName;
            onlineUser.LastName = model.LastName;
            onlineUser.Email = model.Email;
            onlineUser.Address = model.Address;
            onlineUser.ImagePath = model.ImagePath;
            onlineUser.PhoneNumber = model.PhoneNumber;
            onlineUser.CityId = model.CityId;
            onlineUser.UserName = model.UserName;
            onlineUser.Inventory = model.Price;


            var expert = await _expertAppService.GetByIdAsync(onlineUser.Id, cancellationToken);

            var result = await _expertAppService.UpdateAsync(expert,model.Skills,cancellationToken);

            if (result)
            {
                ViewBag.UpdateMessage = "information updated successfully";
                return RedirectToAction("EditExpertInfo");
            }
            ViewBag.UpdateMessage = "information did not updated ";

            return RedirectToAction("EditExpertInfo");
        }
    }
}

