﻿using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AChareh.Domain.Core.Contracts.Service
{
    public interface IHomeServiceDapperService
    {
        Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken);
        //Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken);
    }
}
