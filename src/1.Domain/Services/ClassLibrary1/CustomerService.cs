﻿using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CustomerService(ICustomerRepository customerRepository, UserManager<User> userManager)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
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

        public Task<IdentityResult> RegisterAsync(User user, string pass)
        {
            return _userManager.CreateAsync(user, pass);
        }
        public Task<IdentityResult> UpdateAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)

            => await _customerRepository.GetCustomerByIdAsync(id, cancellationToken);

        public async Task<bool> InventoryReductionAsync(int userId, double inventoryReduction, CancellationToken cancellationToken)

            => await _customerRepository.InventoryReductionAsync(userId, inventoryReduction, cancellationToken);
    }
}
