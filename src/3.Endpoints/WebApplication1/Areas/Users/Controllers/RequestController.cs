using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Achareh.Domain.Services;
using Achareh.Endpoint.MVC.Areas.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class RequestController : Controller
    {
        private readonly IRequestAppService _requestAppService;
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        private readonly ICustomerAppService _customerAppService;
        private readonly IHomeServiceAppService _homeServiceAppService;



        public RequestController(IRequestAppService requestAppService, IImageService imageService, UserManager<User> userManager, ICustomerAppService customerAppService, IHomeServiceAppService homeServiceAppService)
        {
            _requestAppService = requestAppService;
            _imageService = imageService;
            _userManager = userManager;
            _customerAppService = customerAppService;
            _homeServiceAppService = homeServiceAppService;
        }


        [HttpGet]
        public async Task<IActionResult> AddRequest(int homeServiceId , CancellationToken cancellationToken)
        {
            var homeService = await _homeServiceAppService.GetByIdAsync(homeServiceId, cancellationToken);
            var request = new AddRequestViewModel()
            {
                HomeServiceId = homeService.Id,
                HomeServiceTitle = homeService.Title

            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(AddRequestViewModel model,CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
               
                return View(model);
            }
            if (model.ImageFiles is not null && model.ImageFiles.Any())
            {
                model.ImagePaths = new List<string>();

                foreach (var imageFile in model.ImageFiles)
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var imagePath = await _imageService.UploadImage(imageFile!, "request", cancellationToken);
                        model.ImagePaths.Add(imagePath);
                    }
                   
                }

               
            }
           
            var requestDateTime = model.RequestForDate.Date + model.RequestForTime.TimeOfDay;

            var onlineUser = await _userManager.GetUserAsync(User);
            if (onlineUser == null)
            {
                ModelState.AddModelError("", "مشتری یافت نشد.");   //beporsim
                return View(model);
            }

            var homeService = await _homeServiceAppService.GetByIdAsync(model.HomeServiceId, cancellationToken);
            if (homeService == null)
            {
                ModelState.AddModelError("", "سرویس خانگی یافت نشد.");
                return View(model);
            }
            var cityId = onlineUser.CityId;
            
            var customer = await _customerAppService.GetCustomerByIdAsync(onlineUser.Id, cancellationToken);
            if (customer == null)
            {
                ModelState.AddModelError("", "مشتری یافت نشد.");
                return View(model);
            }

            var newRequest = new Request
            {
                Title = model.HomeServiceTitle,
                RequestForTime = requestDateTime,
                RequestImages = model.ImagePaths,
                Description = model.Description,
                HomeServiceId = homeService.Id,
                CityId = cityId,
                CustomerId = customer.Id

            };

            var result = await _requestAppService.CreateAsync(newRequest, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "مشکلی در ایجاد درخواست رخ داده است.");
                return View(model);
            }

            return RedirectToAction("RequestList","CustomerDashboard");


        }
    }
}
