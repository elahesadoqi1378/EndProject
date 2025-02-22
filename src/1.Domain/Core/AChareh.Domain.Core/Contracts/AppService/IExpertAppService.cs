﻿using Achareh.Domain.Core.Entities.User;
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
        Task<int> GetCount(CancellationToken cancellationToken);
        Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken);

    }
}
