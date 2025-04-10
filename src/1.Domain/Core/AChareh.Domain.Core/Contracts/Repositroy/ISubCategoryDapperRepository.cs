using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AChareh.Domain.Core.Contracts.Repositroy
{
    public interface ISubCategoryDapperRepository
    {
        Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken);
    }
}
