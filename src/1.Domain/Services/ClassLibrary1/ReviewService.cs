
using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Dtos.Review;
using Achareh.Domain.Core.Entities.Request;

namespace Achareh.Domain.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> Accept(int id, CancellationToken cancellationToken)

            => await _reviewRepository.Accept(id, cancellationToken);
              
        public async Task CreateAsync(CreateReviewDto review, CancellationToken cancellationToken)

            => await _reviewRepository.CreateAsync(review, cancellationToken);

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)

            => await _reviewRepository.DeleteAsync(id, cancellationToken);

        public async Task<List<Review>> ReviewInfo(CancellationToken cancellationToken)

            => await _reviewRepository.ReviewInfo(cancellationToken);

        public async Task<List<Review>> GetAllAsync(CancellationToken cancellationToken)

            => await _reviewRepository.GetAllAsync(cancellationToken);
            
        public async Task<Review> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _reviewRepository.GetByIdAsync(id, cancellationToken);

        public async Task<bool> RatingSet(int expertId, int rate, CancellationToken cancellationToken)

            => await _reviewRepository.RatingSet(expertId,rate,cancellationToken);

        public async Task<bool> Reject(int id, CancellationToken cancellationToken)

            => await _reviewRepository.Reject(id, cancellationToken);

        public async Task UpdateAsync(UpdateReviewDto review, CancellationToken cancellationToken)

           => await _reviewRepository.UpdateAsync(review, cancellationToken);
    }
}
