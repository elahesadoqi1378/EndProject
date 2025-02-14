using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Repositroy
{
    public interface IHomeServiceRepository
    {
        Task<List<HomeService>> GetAllAsync();
        Task<HomeService> GetByIdAsync(int id);
        Task<bool> CreateAsync(HomeService homeService);
        Task<bool> UpdateAsync(HomeService homeService);
        Task<bool> DeleteAsync(int id);
    }
}
