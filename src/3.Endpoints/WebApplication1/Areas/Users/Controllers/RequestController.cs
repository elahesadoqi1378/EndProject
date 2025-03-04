using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Endpoint.MVC.Areas.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class RequestController : Controller
    {
        private readonly IRequestAppService _requestAppService;
        private readonly IImageService _imageService;

        public RequestController(IRequestAppService requestAppService, IImageService imageService)
        {
            _requestAppService = requestAppService;
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult RequestIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(AddRequestViewModel model ,CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
               
                return View(model);
            }
            if (model.ImageFiles is not null)
            {
                model.ImagePaths = new List<string>();
                foreach (var imageFile in model.ImageFiles)
                {
                    var imagePath = await _imageService.UploadImage(imageFile!, "request", cancellationToken);
                    model.ImagePaths.Add(imagePath);
                }

               
            }
            var requestDateTime = model.RequestForDate.Date + model.RequestForTime.TimeOfDay;
            var newRequest = new Request
            {
                Title = model.HomeServiceTitle,
                RequestForTime = requestDateTime,
                RequestImages = model.ImagePaths is not null ? string.Join(",", model.ImagePaths) : null,
                Description = model.Description
            };

            var result = await _requestAppService.CreateAsync(newRequest, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "مشکلی در ایجاد درخواست رخ داده است.");
                return View(model);
            }

            return RedirectToAction("RequestIndex");


        }
    }
}
