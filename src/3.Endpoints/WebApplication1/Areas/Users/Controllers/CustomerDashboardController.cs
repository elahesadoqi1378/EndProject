using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Dtos.Review;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Achareh.Domain.Core.Enums;
using Achareh.Domain.Services;
using Achareh.Endpoint.MVC.Areas.Users.Models;
using Azure.Core;
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
        private readonly IExpertAppService _expertAppService;
        private readonly IAdminAppService _adminAppService;

        public CustomerDashboardController(UserManager<User> userManager, ICustomerAppService customerAppService, IRequestAppService requestAppService, IExpertOfferAppService expertOfferAppService, ICityAppService cityAppService, IImageService imageService, IReviewAppService reviewAppService, IExpertAppService expertAppService,IAdminAppService adminAppService)
        {
            _userManager = userManager;
            _customerAppService = customerAppService;
            _requestAppService = requestAppService;
            _expertOfferAppService = expertOfferAppService;
            _cityAppService = cityAppService;
            _imageService = imageService;
            _reviewAppService = reviewAppService;
            _expertAppService = expertAppService;
            _adminAppService = adminAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            var onlineUser = await _userManager.GetUserAsync(User);

            if (onlineUser is null)
                return RedirectToAction("Login", "Account");

            var paidOrderCount = await _requestAppService.GetPaidByCustomerOrderCountAsync(onlineUser.Id, cancellationToken);
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
                UserName = onlineUser.UserName,
                Price = onlineUser.Inventory
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
          

            var onlineUser = await _userManager.GetUserAsync(User);
            if (onlineUser is null)
                return RedirectToAction("Login", "Account");


            if (model.ImageFile is null) 
            {
                model.ImagePath = onlineUser.ImagePath; 
            }
            else
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "customer", cancellationToken);
            }

            onlineUser.FirstName = model.FirstName;
            onlineUser.LastName = model.LastName;
            onlineUser.Email = model.Email;
            onlineUser.Address = model.Address;
            onlineUser.ImagePath = model.ImagePath;
            onlineUser.PhoneNumber = model.PhoneNumber;
            onlineUser.CityId = model.CityId;
            onlineUser.UserName = model.UserName;
            onlineUser.Inventory = model.Price;
            var result = await _customerAppService.UpdateAsync(onlineUser);

            if (result.Succeeded)
            {
                ViewBag.UpdateMessage = "information updated successfully";
                return RedirectToAction("EditCustomerInfo");
            }
            ViewBag.UpdateMessage = "information did not updated ";

            return RedirectToAction("EditCustomerInfo");
        }




        public async Task<IActionResult> RequestList(CancellationToken cancellationToken)
        {
            var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (onlineUserId is null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(onlineUserId);

            var requests = await _requestAppService.GetCustomerRequestAsync(userId, cancellationToken); 
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

                var requestResult = await _requestAppService.ChangeStatusOfRequest(StatusEnum.WorkStarted,offer.RequestId, cancellationToken);
                if (requestResult)
                {
                    var winnerResult = await _requestAppService.SetWinnerForRequest(offer.Id,offer.RequestId, cancellationToken);
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
            var winner = await _expertOfferAppService.GetByIdAsync(id, cancellationToken);
            if (winner == null)
                return NotFound();
            ViewBag.Price = winner.SuggestedPrice;
            ViewBag.SuggestionId = winner.Id;
            ViewBag.RequestId = winner.RequestId;


            return View(onlineUser);
        }
        [HttpPost]
        public async Task<IActionResult> RequestPayment(int SuggestionId, int RequestId, double minPrice, string selectedAmount, CancellationToken cancellationToken)
        {
            var onlineUser = await _userManager.GetUserAsync(User);
            if (onlineUser == null)
                return NotFound();

            double price = 0;

            if (!string.IsNullOrEmpty(selectedAmount) && double.TryParse(selectedAmount, out double defaultMoney))
            {
                price = defaultMoney;
            }
            else
            {
                ModelState.AddModelError("", "please enter suitable amount.");
                return View(onlineUser);
            }

            var setWinnerResult = await _requestAppService.SetWinnerForRequest(SuggestionId, RequestId, cancellationToken);
            if (!setWinnerResult)
            {
                TempData["ResultMessage"] = "خطا در تعیین برنده درخواست";
                return RedirectToAction("RequestList");
            }

            var result = await _customerAppService.InventoryReductionAsync(onlineUser.Id, price, cancellationToken);
            if (result)
            {
                var suggestionResult = await _expertOfferAppService.ChangeStausOfExpertOffer(SuggestionId, StatusEnum.WorkPaidByCustomer, cancellationToken);
                if (suggestionResult)
                {
                    var requestResult = await _requestAppService.ChangeStatusOfRequest(StatusEnum.WorkPaidByCustomer, RequestId, cancellationToken);
                    if (requestResult)
                    {
                        var suggestion = await _expertOfferAppService.GetByIdAsync(SuggestionId, cancellationToken);
                        if (suggestion != null)
                        {
                            var expert = await _expertAppService.GetByIdAsync(suggestion.ExpertId, cancellationToken);
                            if (expert != null)
                            {
                                double expertShare = price * 0.7;
                                double adminShare = price * 0.3;

                                var expertIncreaseResult = await _expertAppService.InventoryIncreaseAsync(expert.Id.ToString(), expertShare, cancellationToken);
                                if (expertIncreaseResult)
                                {
                                    var adminIncreaseResult = await _adminAppService.InventoryIncreaseAsync("1", adminShare, cancellationToken);
                                    if (adminIncreaseResult)
                                    {
                                        TempData["ResultMessage"] = "پرداخت وجه موفقیت آمیز بود";
                                        return RedirectToAction("RequestList");
                                    }
                                    else
                                    {
                                        TempData["ResultMessage"] = "خطا در پرداخت وجه: واریز به حساب ادمین صورت نگرفت";
                                        return RedirectToAction("RequestList");
                                    }
                                }
                                else
                                {
                                    TempData["ResultMessage"] = "خطا در پرداخت وجه: واریز به حساب کارشناس صورت نگرفت";
                                    return RedirectToAction("RequestList");
                                }
                            }
                            else
                            {
                                TempData["ResultMessage"] = "خطا در پرداخت وجه: کارشناس یافت نشد";
                                return RedirectToAction("RequestList");
                            }
                        }
                        else
                        {
                            TempData["ResultMessage"] = "خطا در پرداخت وجه: پیشنهاد یافت نشد";
                            return RedirectToAction("RequestList");
                        }
                    }

                    TempData["ResultMessage"] = "خطا در پرداخت وجه: تغییر وضعیت درخواست صورت نگرفت";
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

           
            var request = await _requestAppService.GetByIdAsync(model.RequestId, cancellationToken); 
            if (request != null)
            {
                request.IsReviewd = true;
                await _requestAppService.UpdateAsync(request, cancellationToken); 
            }

            TempData["ResultMessage"] = "your comment succefully registerd.";
            return RedirectToAction("RequestList");
        }
    }
}





