using Achareh.Domain.Core.Dtos.Review;
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllAsync(CancellationToken cancellationToken);
        Task<Review> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> Accept(int id, CancellationToken cancellationToken);
        Task<bool> Reject(int id, CancellationToken cancellationToken);
        Task CreateAsync(CreateReviewDto review, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateReviewDto review, CancellationToken cancellationToken);
        Task<bool> RatingSet(int expertId, int rate, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<Review>> ReviewInfo(CancellationToken cancellationToken);

    }
}
