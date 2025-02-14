using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Repositroy
{
    public interface ICategoryRepositroy
    {
        Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task<Category> GetByIdAsync(int id,CancellationToken cancellationToken );
        Task<Category> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Category category,CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Category category,CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id,CancellationToken cancellationToken);

    }
}
