

using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Review>> GetAllAsync()
        
            => await _context.Reviews.ToListAsync();


        public async Task<Review> GetByIdAsync(int id)

            => await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Review review)
        {
            var existingReview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == review.Id);
            if (existingReview != null)
            {
                existingReview.Rating = review.Rating;
                existingReview.Comment = review.Comment;
                existingReview.IsAccept = review.IsAccept;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                review.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Accept(int reviewId, CancellationToken cancellationToken)
        {
            var existingreview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            existingreview.IsAccept = true;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Reject(int reviewId, CancellationToken cancellationToken)
        {
            var existingreview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            existingreview.IsAccept = false;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
