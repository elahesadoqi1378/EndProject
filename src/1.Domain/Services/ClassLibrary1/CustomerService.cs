using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken)
        
         => await _customerRepository.CreateAsync(customer, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

         => await _customerRepository.DeleteAsync(id, cancellationToken);

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)

         => await _customerRepository.GetAllAsync(cancellationToken);

        public async Task<Customer?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

         => await _customerRepository.GetByIdWithDetailsAsync(id, cancellationToken);

        public async Task<int> GetCount(CancellationToken cancellationToken)

         => await _customerRepository.GetCount(cancellationToken);

        public async Task<Customer?> GetrByIdAsync(int id, CancellationToken cancellationToken)

         => await _customerRepository.GetrByIdAsync(id, cancellationToken);

        public async Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken)

        => await _customerRepository.UpdateAsync(customer, cancellationToken);
          
           
    }
}
