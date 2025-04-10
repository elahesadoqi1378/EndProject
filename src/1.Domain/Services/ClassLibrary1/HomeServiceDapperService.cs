using AChareh.Domain.Core.Contracts.Repositroy;
using AChareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Services
{
    public class HomeServiceDapperService : IHomeServiceDapperService
    {
        private readonly IHomeServiceDapperRepository _homeServiceDapperRepository;

        public HomeServiceDapperService(IHomeServiceDapperRepository homeServiceDapperRepository)
        {
            _homeServiceDapperRepository = homeServiceDapperRepository;
        }

        public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
            => await _homeServiceDapperRepository.GetAllAsync(cancellationToken);

       // public async Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)

       //=> await _homeServiceDapperRepository.GetHomeServicesBySubCategoryId(subCategoryId, cancellationToken);
    }
}
