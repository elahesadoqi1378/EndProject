﻿using Achareh.Domain.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Repositroy
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken);
        Task<Customer?> GetrByIdAsync(int id, CancellationToken cancellationToken);
        Task<Customer?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken);
        Task<int> GetCount(CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken);
        Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> InventoryReductionAsync(int userId, double inventoryReduction, CancellationToken cancellationToken);
    }
}
