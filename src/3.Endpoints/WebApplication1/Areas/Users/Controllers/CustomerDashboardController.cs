using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace Achareh.Endpoint.MVC.Areas.Users.Controllers
{
    [Area("Users")]
    public class CustomerDashboardController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICustomerAppService _customerAppService;
        private readonly IRequestAppService _requestAppService;

        public CustomerDashboardController(UserManager<User> userManager, ICustomerAppService customerAppService, IRequestAppService requestAppService)
        {
            _userManager = userManager;
            _customerAppService = customerAppService;
            _requestAppService = requestAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (onlineUserId is null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(onlineUserId);

            var userInfo = await _customerAppService.GetByIdWithDetailsAsync(userId, cancellationToken); // فرض میکنیم چنین متدی در سرویس شما وجود دارد

            return View(userInfo);
        
        }

        public async Task<IActionResult> RequestList(CancellationToken cancellationToken)
        {
            var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (onlineUserId is null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(onlineUserId);

            var requests = await _requestAppService.GetCustomerRequestAsync(userId, cancellationToken); // فرض میکنیم چنین متدی در سرویس شما وجود دارد

            return View(requests);
        }
        public IActionResult OfferList()
        {
            return View();
        }
        public IActionResult EditCustomerInfo()
        {
            return View();
        }
    }


}
