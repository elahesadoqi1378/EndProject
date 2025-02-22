using Achareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
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
    }
}
