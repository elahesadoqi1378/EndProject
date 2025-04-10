using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.AppServices
{
    public class HomeServiceAppService : IHomeServiceAppService
    {
        private readonly IHomeServiceService _homeServiceService;

        public HomeServiceAppService(IHomeServiceService homeServiceService)
        {
            _homeServiceService = homeServiceService;
        }
        public async Task<bool> CreateAsync(HomeService homeService, CancellationToken cancellationToken)

           => await _homeServiceService.CreateAsync(homeService, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

           => await _homeServiceService.DeleteAsync(id, cancellationToken);

        //public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)

        //   => await _homeServiceService.GetAllAsync(cancellationToken);

        public async Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken)

          => await _homeServiceService.GetByIdAsync(id, cancellationToken);

        public async Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken)

          => await _homeServiceService.UpdateAsync(homeService, cancellationToken);

        public async Task<List<HomeService>> GetAllWithSubCategoryId(int subCategoryId, CancellationToken cancellationToken)

          => await _homeServiceService.GetAllWithSubCategoryId(subCategoryId, cancellationToken);



    }
}
