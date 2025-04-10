using Achareh.Domain.Core.Contracts.AppService;
using AChareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class HomeServiceController : Controller
    {
        private readonly IHomeServiceAppService _homeServiceAppService;
        private readonly IHomeServiceDapperAppService _homeServiceDapperAppService;
        public HomeServiceController(IHomeServiceAppService homeServiceAppService, IHomeServiceDapperAppService homeServiceDapperAppService)
        {
            _homeServiceAppService = homeServiceAppService;
        }
        public async Task<IActionResult> Index(int subCategoryId, CancellationToken cancellationToken)
        {

            var homeServices = await _homeServiceAppService.GetAllWithSubCategoryId(subCategoryId, cancellationToken);

            if (homeServices == null || !homeServices.Any())
            {
                return NotFound();
            }

            return View(homeServices);

        }
    }
}
