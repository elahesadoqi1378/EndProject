
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Dtos.Review;
using Achareh.Domain.Core.Entities.Request;

namespace Achareh.Domain.AppServices
{
    public class ReviewAppService : IReviewAppService
    {
        private readonly IReviewService _reviewService;

        public ReviewAppService(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<bool> Accept(int id, CancellationToken cancellationToken)

            => await _reviewService.Accept(id, cancellationToken);

        public async Task CreateAsync(CreateReviewDto review, CancellationToken cancellationToken)

            => await _reviewService.CreateAsync(review, cancellationToken);

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)

            => await _reviewService.DeleteAsync(id, cancellationToken);

        public async Task<List<Review>> ReviewInfo( CancellationToken cancellationToken)

            => await _reviewService.ReviewInfo( cancellationToken);

        public async Task<List<Review>> GetAllAsync(CancellationToken cancellationToken)

            => await _reviewService.GetAllAsync(cancellationToken);

        public async Task<Review> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _reviewService.GetByIdAsync(id, cancellationToken);

        public async Task<bool> RatingSet(int expertId, int rate, CancellationToken cancellationToken)

            => await _reviewService.RatingSet(expertId, rate, cancellationToken);

        public async Task<bool> Reject(int id, CancellationToken cancellationToken)

            => await _reviewService.Reject(id, cancellationToken);

        public async Task UpdateAsync(UpdateReviewDto review, CancellationToken cancellationToken)

           => await _reviewService.UpdateAsync(review, cancellationToken);


    }
}
