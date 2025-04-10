using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Achareh.Domain.Core.Enums;
using Achareh.Endpoint.MVC.Areas.Users.Models;
using AChareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Composition;
using System.Net;
using System.Security.Claims;
using System.Threading;

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
        private readonly IHomeServiceDapperAppService _homeServiceDapperAppService;
        private readonly IRequestAppService _requestAppService;
        private readonly IExpertOfferAppService _expertOfferAppService;

        public ExpertDashboardController(UserManager<User> userManager, IExpertAppService expertAppService, ICityAppService cityAppService, IHomeServiceAppService homeServiceAppService, IRequestAppService requestAppService, IExpertOfferAppService expertOfferAppService, IHomeServiceDapperAppService homeServiceDapperAppService, IImageService imageService)
        {
            _userManager = userManager;
            _expertAppService = expertAppService;
            _cityAppService = cityAppService;
            _homeServiceAppService = homeServiceAppService;
            _requestAppService = requestAppService;
            _expertOfferAppService = expertOfferAppService;
            _homeServiceDapperAppService = homeServiceDapperAppService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            var expert = User.FindFirstValue(ClaimTypes.NameIdentifier);                   
            var onlineUser = await _userManager.GetUserAsync(User);

            if (onlineUser is null)
                return RedirectToAction("Login", "Account");
            int userId = int.Parse(expert);

            var userInfo = await _expertAppService.GetExpertProfileByIdAsync(onlineUser.Id, cancellationToken);



            return View(userInfo);

        }
        public async Task<IActionResult> ExpertDetail(int expertId, CancellationToken cancellationToken)
        {
            var expert = await _expertAppService.GetExpertProfileByIdAsync(expertId, cancellationToken);
            return View(expert);
        }




        public async Task<IActionResult> EditExpertInfo(CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllAsync(cancellationToken);
            ViewBag.Cities = cities.Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.Title
            }).ToList();

            var homeServices = await _homeServiceDapperAppService.GetAllAsync(cancellationToken);
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
                ImagePath = onlineUser.ImagePath,
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
            var homeServices = await _homeServiceDapperAppService.GetAllAsync(cancellationToken);

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


            if (model.ImageFile is null)
            {
                model.ImagePath = onlineUser.ImagePath;
            }
            else
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

            var result = await _expertAppService.UpdateAsync(expert, model.Skills, cancellationToken);

            if (result)
            {
                ViewBag.UpdateMessage = "information updated successfully";
                return RedirectToAction("EditExpertInfo");
            }
            ViewBag.UpdateMessage = "information did not updated ";

            return RedirectToAction("EditExpertInfo");
        }

        public async Task<IActionResult> ShowRequests(CancellationToken cancellationToken)
        {
            var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (onlineUserId is null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(onlineUserId);
            var expert = await _expertAppService.GetExpertByIdWithDetailsAsync(userId, cancellationToken);
            if (expert?.HomeServices == null)
            {
                return RedirectToAction("Index");
            }

            var expertHomeServices = expert.HomeServices.Select(x => x.Id).ToList();
            var requests = await _requestAppService.GetRequestsByHomeServices(expertHomeServices,expert.User.CityId,cancellationToken);

            return View(requests);

        }

        public async Task<IActionResult> SendOffer(int requestId, CancellationToken cancellationToken)
        {
            var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (onlineUserId is null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(onlineUserId);
            var expert = await _expertAppService.GetExpertByIdWithDetailsAsync(userId, cancellationToken);

            if (expert == null)
            {
                return NotFound("Expert not found.");
            }

            var request = await _requestAppService.GetByIdAsync(requestId, cancellationToken);

            if (request == null )
            {
                return NotFound("Request not found.");
            }

          

            var model = new OfferViewModel
            {
                RequestId = request.Id,
                OfferDate = DateTime.Now,
                ExpertId = expert.Id
            };

            return View(model);
        }
        

        [HttpPost]
        public async Task<IActionResult> SendOffer(OfferViewModel model, CancellationToken cancellationToken)
        {
            var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (onlineUserId is null)
                return RedirectToAction("Login", "Account");

            int expertId = int.Parse(onlineUserId);

            var newOffer = new ExpertOffer
            {
                ExpertId = model.ExpertId,
                RequestId = model.RequestId,
                SuggestedPrice = model.SuggestedPrice,
                Description = model.Description,
                OfferDate = model.OfferDate
            };

            var result = await _expertOfferAppService.CreateAsync(newOffer, cancellationToken);

            if (!result)
            {
                ModelState.AddModelError("", "مشکلی در ایجاد پیشنهاد رخ داده است.");
                return View(model);
            }
            var requestResult = await _requestAppService.ChangeStatusOfRequest(StatusEnum.WatingForCustomerToChoose, newOffer.RequestId, cancellationToken);
            if (!requestResult)
            {
                ModelState.AddModelError("", "خطا رخ داده است.");
                return View(model);
            }

            return RedirectToAction("ShowRequests", "ExpertDashboard");

        }


        public async Task<IActionResult> RequestDetails(int id, CancellationToken cancellationToken)
        {
            var request = await _requestAppService.GetByIdAsync(id, cancellationToken);
            return View(request);
        }

     
    }

}


