using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Dtos.Review;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Achareh.Domain.Core.Enums;
using Achareh.Endpoint.MVC.Areas.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IExpertOfferAppService _expertOfferAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IImageService _imageService;
        private readonly IReviewAppService _reviewAppService;

        public CustomerDashboardController(UserManager<User> userManager, ICustomerAppService customerAppService, IRequestAppService requestAppService, IExpertOfferAppService expertOfferAppService, ICityAppService cityAppService, IImageService imageService, IReviewAppService reviewAppService)
        {
            _userManager = userManager;
            _customerAppService = customerAppService;
            _requestAppService = requestAppService;
            _expertOfferAppService = expertOfferAppService;
            _cityAppService = cityAppService;
            _imageService = imageService;
            _reviewAppService = reviewAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            var onlineUser = await _userManager.GetUserAsync(User);

            if (onlineUser is null)
                return RedirectToAction("Login", "Account");

            var paidOrderCount = await _requestAppService.GetPaidByCustomerOrderCountAsync(onlineUser.Id,cancellationToken);
            ViewBag.PaidOrderCount = paidOrderCount;

            int userId = onlineUser.Id;

            var userInfo = await _customerAppService.GetByIdWithDetailsAsync(userId, cancellationToken); 

            return View(userInfo);

        }
        public async Task<IActionResult> EditCustomerInfo(CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllAsync(cancellationToken);
            ViewBag.Cities = cities.Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.Title
            }).ToList();

            var onlineUser = await _userManager.GetUserAsync(User);                                                                //////////////amaliyat haye tekrari baresi shavad


            if (onlineUser is null)
                return RedirectToAction("Login", "Account");

            var model = new EditCustomerInfoViewModel
            {
                FirstName = onlineUser.FirstName,
                LastName = onlineUser.LastName,
                Email = onlineUser.Email,
                Address = onlineUser.Address,
                ImagePath = onlineUser.ImagePath,
                PhoneNumber = onlineUser.PhoneNumber,
                CityId = onlineUser.CityId,
                UserName = onlineUser.UserName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomerInfo(EditCustomerInfoViewModel model, CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllAsync(cancellationToken);

            if (!ModelState.IsValid)
            {
                ViewBag.Cities = cities.Select(sc => new SelectListItem
                {
                    Value = sc.Id.ToString(),
                    Text = sc.Title
                }).ToList();
                return View(model);

            }

            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "customer", cancellationToken);
            }

            var onlineUser = await _userManager.GetUserAsync(User);
            if (onlineUser is null)
                return RedirectToAction("Login", "Account");

            onlineUser.FirstName = model.FirstName;
            onlineUser.LastName = model.LastName;
            onlineUser.Email = model.Email;
            onlineUser.Address = model.Address;
            onlineUser.ImagePath = model.ImagePath;
            onlineUser.PhoneNumber = model.PhoneNumber;
            onlineUser.CityId = model.CityId;
            onlineUser.UserName = model.UserName;
            var result = await _customerAppService.UpdateAsync(onlineUser);

            if (result.Succeeded)
            {
                ViewBag.UpdateMessage = "information updated successfully";
                return RedirectToAction("EditCustomerInfo");
            }
            ViewBag.UpdateMessage = "information didi not updated ";

            return RedirectToAction("EditCustomerInfo");
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
        public async Task<IActionResult> RequestDetails(int id, CancellationToken cancellationToken)
        {
            var request = await _requestAppService.GetByIdAsync(id, cancellationToken);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }
        public async Task<IActionResult> OfferList(int id, CancellationToken cancellationToken)
        {
            var offers = await _expertOfferAppService.OffersOfRequest(id, cancellationToken);

            return View(offers);
        }

        public async Task<IActionResult> OfferDetails(int id, CancellationToken cancellationToken)
        {
            var expertOffer = await _expertOfferAppService.GetByIdAsync(id, cancellationToken);
            if (expertOffer == null)
            {
                return NotFound();
            }
            return View(expertOffer);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOffer(int id, CancellationToken cancellationToken)
        {


            var offer = await _expertOfferAppService.GetByIdAsync(id, cancellationToken);

            if (offer == null)
            {
                return NotFound();
            }

            var offerResult = await _expertOfferAppService.ChangeStausOfExpertOffer(offer.Id, StatusEnum.WorkStarted, cancellationToken);

            if (offerResult)
            {

                var requestResult = await _requestAppService.ChangeStatus(offer.RequestId, (int)StatusEnum.WorkStarted, cancellationToken);
                if (requestResult)
                {
                    var winnerResult = await _requestAppService.SetWinnerForRequest(offer.RequestId, offer.Id, cancellationToken);
                    TempData["ResultMessage"] = "Result was successfull and Winner request selected";
                    return RedirectToAction("RequestList");
                }

                TempData["ResultMessage"] = "Result was not successfull and Winner request did not selecte";


                TempData["ResultMessage"] = "Result was successfull and Winner offer selected";
                return RedirectToAction("RequestList");

            }

            TempData["ResultMessage"] = "Result was not successfull and Winner offer did not selecte";
            return RedirectToAction("RequestList");
        }

        public async Task<IActionResult> RequestPayment(int id, CancellationToken cancellationToken)
        {
            var onlineUser = await _userManager.GetUserAsync(User);
            if (onlineUser is null)
                return RedirectToAction("Login", "Account");

            var winnerOffer = await _expertOfferAppService.GetByIdAsync(id, cancellationToken);
            if (winnerOffer == null)
                return NotFound();

            ViewBag.SuggestedPrice = winnerOffer.SuggestedPrice;
            ViewBag.offerId = winnerOffer.Id;
            ViewBag.RequestId = winnerOffer.RequestId;


            return View(onlineUser);
        }

        [HttpPost]
        public async Task<IActionResult> RequestPayment(int offerId, string suggestedAmount, string customAmount, CancellationToken cancellationToken)
        {
            var onlineUser = await _userManager.GetUserAsync(User);
            if (onlineUser == null)
                return NotFound();

            double price = 0;

            if (!string.IsNullOrEmpty(customAmount) && double.TryParse(customAmount, out double customMoney) && customMoney > 0)
            {
                price = customMoney;
            }
            else if (!string.IsNullOrEmpty(suggestedAmount) && double.TryParse(suggestedAmount, out double defaultMoney))
            {
                price = defaultMoney;
            }
            else
            {
                ModelState.AddModelError("", "please enter suitable amount.");
                return View(onlineUser);
            }

            var winnerOffer = await _expertOfferAppService.GetByIdAsync(offerId, cancellationToken); 
            if (winnerOffer == null)
            {
                ModelState.AddModelError("", "offer winner didnot found.");
                return View(onlineUser);
            }

            var request = winnerOffer.Request; 
            if (request == null)
            {
                ModelState.AddModelError("", "request of the winner offer did not found.");
                return View(onlineUser);
            }


            if (onlineUser.Inventory < price) 
            {
                ModelState.AddModelError("customAmount", "your inventory is not enough.");
                return View(onlineUser);
            }

            var result = await _customerAppService.InventoryReductionAsync(onlineUser.Id, price, cancellationToken);
            if (result)
            {
                var suggestionResult = await _expertOfferAppService.ChangeStausOfExpertOffer(offerId, StatusEnum.WorkPaidByCustomer, cancellationToken);
                if (suggestionResult)
                {
                    var requestResult = await _requestAppService.ChangeStatusOfRequest(StatusEnum.WorkPaidByCustomer,request.Id, cancellationToken); 
                    if (requestResult)
                    {
                        TempData["ResultMessage"] = "paying money was successfull";
                        TempData["OfferId"] = offerId;
                        TempData["RequestId"] = request.Id; 
                        return RedirectToAction("SetReview", new { requestId = request.Id, expertId = offerId }); 
                    }

                    TempData["ResultMessage"] = "error in paying money ,something is wrong";
                    return RedirectToAction("RequestList");
                }
            }
            TempData["PaymentResult"] = "خطا در پرداخت وجه: تغییر وضعیت پیشنهاد صورت نگرفت";
            return RedirectToAction("RequestList");
        }
        public async Task<IActionResult> SetReview(int requestId, int expertId)
        {
            var onlineUser = await _userManager.GetUserAsync(User);

            if (onlineUser is null)
                return RedirectToAction("Login", "Account");

            var model = new CreateReviewDto
            {
                RequestId = requestId,
                ExpertId = expertId
            };

            ViewBag.UserName = $"{onlineUser.FirstName} {onlineUser.LastName}";

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SetReview(CreateReviewDto model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var onlineUser = await _userManager.GetUserAsync(User);

            if (onlineUser is null)
                return RedirectToAction("Login", "Account");

            var customer = await _customerAppService.GetrByIdAsync(onlineUser.Id, cancellationToken);

            if (customer == null)
            {
               
                ModelState.AddModelError(string.Empty, "customer with this property didnot found.");
                return View(model);
            }



            model.CustomerId = customer.Id;

            await _reviewAppService.CreateAsync(model, cancellationToken);

            TempData["ResultMessage"] = "your comment succefully registerd.";
            return RedirectToAction("RequestList");
        }
    }
}





