using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class RequestController : Controller
    {
        public IActionResult AddRequest()
        {
            return View();
        }
    }
}
