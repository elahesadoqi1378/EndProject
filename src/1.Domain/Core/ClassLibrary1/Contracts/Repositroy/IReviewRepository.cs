using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Repositroy
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task Accept(int reviewId, CancellationToken cancellationToken);
        Task Reject(int reviewId, CancellationToken cancellationToken);
        Task CreateAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);

    }
}
