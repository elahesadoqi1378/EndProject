using Achareh.Domain.Core.Contracts.AppService;
using AChareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
    {
        private readonly ICategoryAppService _categoryAppService;
        private readonly ICategoryDapperAppService _categoryDapperAppService;
        public HomeController(ICategoryAppService categoryAppService, ICategoryDapperAppService categoryDapperAppService)
        {
            _categoryAppService = categoryAppService;
            _categoryDapperAppService = categoryDapperAppService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllWithSubCategoriesAsync(cancellationToken);
            return View(categories);
        }
    }
}
