using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.AppServices
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;

        public CustomerAppService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken)

            => await _customerService.CreateAsync(customer, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _customerService.DeleteAsync(id, cancellationToken);

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)

            => await _customerService.GetAllAsync(cancellationToken);

        public async Task<Customer?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

             => await _customerService.GetByIdWithDetailsAsync(id, cancellationToken);

        public async Task<int> GetCount(CancellationToken cancellationToken)

            => await _customerService.GetCount(cancellationToken);

        public async Task<Customer?> GetrByIdAsync(int id, CancellationToken cancellationToken)

            => await _customerService.GetrByIdAsync(id, cancellationToken);

        public Task<IdentityResult> RegisterAsync(User user, string pass)
        {
            return _customerService.RegisterAsync(user, pass);
        }
        public Task<IdentityResult> UpdateAsync(User user)
        {
            return _customerService.UpdateAsync(user);
        }

        public async Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken)

           => await _customerService.UpdateAsync(customer, cancellationToken);

        public async Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)

          => await _customerService.GetCustomerByIdAsync(id, cancellationToken);

    
    }
}
