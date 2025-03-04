using Achareh.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Achareh.Endpoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReviewController : Controller
    {
        private readonly IReviewAppService _reviewAppService;

        public ReviewController(IReviewAppService reviewAppService)
        {
            _reviewAppService = reviewAppService;
        }
        public async Task<IActionResult> ReviewIndex(CancellationToken cancellationToken)
        {
            var reviews = await _reviewAppService.ReviewInfo(cancellationToken);
            return View(reviews);
        }

   
        public async Task<IActionResult> Accept(int id, CancellationToken cancellationToken)
        {
            var review = await _reviewAppService.GetByIdAsync(id, cancellationToken);
            if (review == null)
                return NotFound();

            return View(review);
        }

        
        [HttpPost]
        public async Task<IActionResult> AcceptConfirmed(int id, CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return View();
            }

            var result = await _reviewAppService.Accept(id, cancellationToken);

            if (result)
            {
                return RedirectToAction("ReviewIndex");
            }

            ModelState.AddModelError("", "Something went wrong while accepting the comment.");
            return View();
        }

        public async Task<IActionResult> Reject(int id, CancellationToken cancellationToken)
        {
            var review = await _reviewAppService.GetByIdAsync(id, cancellationToken);
            if (review == null)
                return NotFound();

            return View(review);
        }


        [HttpPost]
        public async Task<IActionResult> RejectConfirmed(int id, CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return View();
            }

            var result = await _reviewAppService.Reject(id, cancellationToken);

            if (result)
            {
                return RedirectToAction("ReviewIndex");
            }

            ModelState.AddModelError("", "Something went wrong while rejecting the comment.");
            return View();
        }


    }
}
