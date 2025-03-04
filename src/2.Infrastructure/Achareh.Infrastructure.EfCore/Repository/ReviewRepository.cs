

using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Dtos.Review;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.Design;
using System.Threading;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReviewRepository> _logger;

        public ReviewRepository(AppDbContext context, ILogger<ReviewRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<Review>> GetAllAsync(CancellationToken cancellationToken)
        
            => await _context.Reviews.ToListAsync(cancellationToken);


        public async Task<Review> GetByIdAsync(int id,CancellationToken cancellationToken)

           => await _context.Reviews
                            .Include(x => x.Expert)
                            .ThenInclude(x => x.User)
                            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task CreateAsync(CreateReviewDto review,CancellationToken cancellationToken)
        {
            var newReview = new Review()
            {
                Title = review.Title,
                Comment = review.Comment,
                Rating = review.Rating,
                ExpertId = review.ExpertId,
                CustomerId = review.CustomerId
            };

            await _context.Reviews.AddAsync(newReview, cancellationToken);
            await _context.SaveChangesAsync();
            _logger.LogInformation("review created Succesfully");
        }
        public async Task UpdateAsync(UpdateReviewDto review , CancellationToken cancellationToken)
        {
            var existingReview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id ==review.Id);
            if (existingReview != null)
            {
                existingReview.Rating = review.Rating;
                existingReview.Comment = review.Comment;
                existingReview.Title = review.Title;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("review updated Succesfully");
            }
        }

        public async Task DeleteAsync(int id,CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                _logger.LogInformation("review deleted Succesfully");
            }
        }

        public async Task<bool> Accept(int id, CancellationToken cancellationToken)
        {
            try
            {
                var existingreview = await _context.Reviews
                                                   .FirstOrDefaultAsync(x => x.Id == id);
                if (existingreview is null)
                    return false;

                existingreview.IsAccept = true;

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("review accepted Succesfully");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in reject review", ex.Message);
                return false;
            }

        }

        public async Task<bool> Reject(int id, CancellationToken cancellationToken)
        {
            try
            {
                var existingreview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
                existingreview.IsAccept = false;
                existingreview.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("review rejected Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in reject review", ex.Message);
                return false;
            }

        }

        public async Task<bool> RatingSet(int expertId, int rate, CancellationToken cancellationToken)
        {
            try
            {
                var existingReview = await _context.Reviews.FirstOrDefaultAsync(x => x.ExpertId == expertId, cancellationToken);

                if (existingReview is null)
                {
                    return false;
                }

                existingReview.Rating = rate;

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("reviews rating setted Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in seeting rate for review", ex.Message);
                return false;
            }


        }

        public async Task<List<Review>> ReviewInfo(CancellationToken cancellationToken)

            => await _context.Reviews
                             .Include(x => x.Expert)
                             .ThenInclude(x => x.User)
                             .Where(x => x.IsAccept == false)
                             .ToListAsync(cancellationToken);
    }
}
