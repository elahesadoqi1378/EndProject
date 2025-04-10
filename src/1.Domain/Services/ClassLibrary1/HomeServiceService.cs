

using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;

namespace Achareh.Domain.Services
{
    public class HomeServiceService : IHomeServiceService
    {
        private readonly IHomeServiceRepository _homeServiceRepository;

        public HomeServiceService(IHomeServiceRepository homeServiceRepository)
        {
            _homeServiceRepository = homeServiceRepository;
        }
        public async Task<bool> CreateAsync(HomeService homeService, CancellationToken cancellationToken)

            => await _homeServiceRepository.CreateAsync(homeService, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _homeServiceRepository.DeleteAsync(id, cancellationToken);

        //public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)

        //   => await _homeServiceRepository.GetAllAsync(cancellationToken);

      
        public async Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken)

          => await _homeServiceRepository.GetByIdAsync(id, cancellationToken);

        public async Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken)

         => await _homeServiceRepository.UpdateAsync(homeService, cancellationToken);

        public async Task<List<HomeService>> GetAllWithSubCategoryId(int subCategoryId, CancellationToken cancellationToken)

        => await _homeServiceRepository.GetAllWithSubCategoryId(subCategoryId, cancellationToken);

    }
}
