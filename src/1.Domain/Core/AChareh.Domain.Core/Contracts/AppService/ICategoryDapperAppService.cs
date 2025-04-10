﻿using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AChareh.Domain.Core.Contracts.AppService
{
   
        public interface ICategoryDapperAppService
        {
            Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
            //Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken);
        }
    
}
