using Achareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IExpertAppService _expertAppService;
        private readonly ICustomerAppService _customerAppService;

        public HomeController(ICustomerAppService customerAppService, IExpertAppService expertAppService)
        {
            _customerAppService = customerAppService;
            _expertAppService = expertAppService;
        }

      
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var customerCount = await _customerAppService.GetCount(cancellationToken);
            //ViewBag.expertCount = await _expertAppService.GetCount(cancellationToken);

             return View(customerCount);

        }
       
    }
}
