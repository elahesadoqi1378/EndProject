using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Enums;
using Achareh.Endpoint.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RequestController : Controller
    {
        private readonly IRequestAppService _requestAppService;
        private readonly IExpertOfferAppService _expertOfferAppService;

        public RequestController(IRequestAppService requestAppService , IExpertOfferAppService expertOfferAppService)
        {
            _expertOfferAppService = expertOfferAppService;
            _requestAppService = requestAppService;
            
        }

        public async Task<IActionResult> RequestIndex(CancellationToken cancellationToken)
        {
            var requests = await _requestAppService.GetRequestsInfo(cancellationToken);
            return View(requests);   
        }
        public async Task<IActionResult> RequestOffers(int requestId , CancellationToken cancellationToken)
        {
            var offers = await _expertOfferAppService.OffersOfRequest(requestId, cancellationToken);
            return View(offers);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int requestId, CancellationToken cancellationToken)
        {
            var request = await _requestAppService.GetByIdAsync(requestId, cancellationToken);

            if (request == null)
            {
                return NotFound();
            }

            var model = new RequestStatusViewModel
            {
                RequestId = request.Id,
                CurrentStatus = request.RequestStatus,
                AvailableStatuses = Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().ToList()
            };

            return RedirectToAction("RequestIndex"); ;
        }


        [HttpPost]
        public async Task<IActionResult> ChangeStatus(RequestStatusViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is NOT valid!");
                return View(model);
            }

            
            var request = await _requestAppService.GetByIdAsync(model.RequestId, cancellationToken);
            if (request == null)
            {
                return NotFound();
            }

         
            if (request.RequestStatus != model.CurrentStatus)
            {
                ModelState.AddModelError(string.Empty, "وضعیت این درخواست قبلاً تغییر کرده است و دیگر قابل تغییر نیست.");
                return View(model);
            }

           
            var result = await _requestAppService.ChangeStatus((int)model.NewStatus, model.RequestId, cancellationToken);

            if (result)
            {
                return RedirectToAction("RequestIndex");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "خطایی در تغییر وضعیت رخ داده است.");
            }

            return RedirectToAction("RequestIndex");
        }


    }
}
