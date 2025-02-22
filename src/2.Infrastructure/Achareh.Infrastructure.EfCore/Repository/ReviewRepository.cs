

using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Dtos.Review;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Threading;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
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
            }
        }

        public async Task DeleteAsync(int id,CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Accept(int id, CancellationToken cancellationToken)
        {
            var existingreview = await _context.Reviews
                                                       .FirstOrDefaultAsync(x => x.Id == id);
            if (existingreview is null)
                return false;

            existingreview.IsAccept = true;
                                                       
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Reject(int id, CancellationToken cancellationToken)
        {
            var existingreview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            existingreview.IsAccept = false;
            existingreview.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> RatingSet(int expertId, int rate, CancellationToken cancellationToken)
        {
            var existingReview = await _context.Reviews.FirstOrDefaultAsync(x => x.ExpertId == expertId, cancellationToken);

            if(existingReview is null)
            {
                return false;
            }

            existingReview.Rating = rate;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
           
        }

        public async Task<List<Review>> ReviewInfo(CancellationToken cancellationToken)

            => await _context.Reviews
                             .Include(x => x.Expert)
                             .ThenInclude(x => x.User)
                             .Where(x => x.IsAccept == false)
                             .ToListAsync(cancellationToken);
    }
}
