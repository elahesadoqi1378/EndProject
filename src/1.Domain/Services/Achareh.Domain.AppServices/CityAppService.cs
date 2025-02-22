using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.AppServices
{
    public class CityAppService : ICityAppService
    {
        private readonly ICityService _cityService;

        public CityAppService(ICityService cityService)
        {
            _cityService = cityService;
        }
        public async Task<bool> CreateAsync(City city, CancellationToken cancellationToken)

            => await _cityService.CreateAsync(city, cancellationToken);
            
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _cityService.DeleteAsync(id, cancellationToken);

        public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)

              => await _cityService.GetAllAsync(cancellationToken);

        public async Task<bool> UpdateAsync(City city, CancellationToken cancellationToken)

              => await _cityService.UpdateAsync(city, cancellationToken);
    }
}
