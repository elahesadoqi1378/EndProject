using Achareh.Domain.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Repositroy
{
    public interface IAdminRepository
    {
        Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken);
        Task<Admin?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Admin admin, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int admin, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Admin admin, CancellationToken cancellationToken);
        Task<bool> InventoryIncreaseAsync(string userId, double amount, CancellationToken cancellationToken);
    }
}
