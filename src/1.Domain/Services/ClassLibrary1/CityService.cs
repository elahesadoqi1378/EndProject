using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<bool> CreateAsync(City city, CancellationToken cancellationToken)

            => await _cityRepository.CreateAsync(city, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

               => await _cityRepository.DeleteAsync(id, cancellationToken);

        public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)

               => await _cityRepository.GetAllAsync(cancellationToken);

        public async Task<bool> UpdateAsync(City city, CancellationToken cancellationToken)

               => await _cityRepository.UpdateAsync(city, cancellationToken);
    }
}
