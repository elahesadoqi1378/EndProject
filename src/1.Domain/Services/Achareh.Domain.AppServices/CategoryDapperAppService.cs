using AChareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AChareh.Domain.Core.Contracts.AppService;


namespace Achareh.Domain.AppServices
{
    public class CategoryDapperAppService : ICategoryDapperAppService
    {
        private readonly ICategoryDapperService _categoryDapperService;

        public CategoryDapperAppService(ICategoryDapperService categoryDapperService)
        {
            _categoryDapperService = categoryDapperService;
        }

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
            => await _categoryDapperService.GetAllAsync(cancellationToken);

        //public async Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
        //    => await _categoryDapperService.GetAllWithDetailsAsync(cancellationToken);
    }
}
