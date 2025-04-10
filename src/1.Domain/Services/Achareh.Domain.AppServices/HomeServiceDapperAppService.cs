using AChareh.Domain.Core.Contracts.AppService;
using AChareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.AppServices
{
    public class HomeServiceDapperAppService : IHomeServiceDapperAppService
    {
        private readonly IHomeServiceDapperService _homeServiceDapperService;

        public HomeServiceDapperAppService(IHomeServiceDapperService homeServiceDapperService)
        {
            _homeServiceDapperService = homeServiceDapperService;
        }

        public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
            => await _homeServiceDapperService.GetAllAsync(cancellationToken);

        //public async Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
        //    => await _homeServiceDapperService.GetHomeServicesBySubCategoryId(subCategoryId, cancellationToken);
    }
}
