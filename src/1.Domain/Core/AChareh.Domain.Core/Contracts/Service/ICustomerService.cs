using Achareh.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken);
        Task<Customer?> GetrByIdAsync(int id, CancellationToken cancellationToken);
        Task<Customer?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken);
        Task<int> GetCount(CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken);
        Task<IdentityResult> RegisterAsync(User user, string pass);
        Task<IdentityResult> UpdateAsync(User user);
        Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> InventoryReductionAsync(int userId, double inventoryReduction, CancellationToken cancellationToken);


    }
       
}
