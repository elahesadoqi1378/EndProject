﻿using Achareh.Domain.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface ICityService
    {
        Task<List<City>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> CreateAsync(City city, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(City city, CancellationToken cancellationToken);
    }
}
