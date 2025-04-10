using Achareh.Domain.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AChareh.Domain.Core.Contracts.Repositroy
{
    public interface ICityDapperRepository
    {
        Task<List<City>> GetAllAsync(CancellationToken cancellationToken);
        Task<City?> GetByIdAsync(int cityId, CancellationToken cancellationToken);
    }
}
