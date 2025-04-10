using Achareh.Domain.Core.Dtos.Category;
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface ICategoryService
    {
        //Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);
        Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Category> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Category category, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<Category>> GetAllWithSubCategoriesAsync(CancellationToken cancellationToken);



    }
}
