using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class CustomerDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
