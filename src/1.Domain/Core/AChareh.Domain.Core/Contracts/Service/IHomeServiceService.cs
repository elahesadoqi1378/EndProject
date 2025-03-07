
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface IHomeServiceService
    {
        Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken);
        Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(HomeService homeService, CancellationToken cancellationToken);
        
        Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<HomeService>> GetAllWithSubCategoryId(int subCategoryId, CancellationToken cancellationToken);

    }
}
