using Achareh.Domain.Core.Entities.User;
using AChareh.Domain.Core.Dtos.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.AppService
{
    public interface IExpertAppService
    {
        Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken);
        Task<Expert?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Expert?> GetExpertByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
        Task<int> GetCount(CancellationToken cancellationToken);
        Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Expert expert, List<int> selectedHomeServiceIds, CancellationToken cancellationToken);
        Task<IdentityResult> RegisterAsync(User user, string pass);
        Task<IdentityResult> UpdateAsync(User user);
        Task<bool> InventoryIncreaseAsync(string userId, double amount, CancellationToken cancellationToken);
        Task<EditExpertDto?> GetExpertProfileByIdAsync(int id, CancellationToken cancellationToken);



}
}
