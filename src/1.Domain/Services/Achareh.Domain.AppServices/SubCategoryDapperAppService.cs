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
    public class SubCategoryDapperAppService : ISubCategoryDapperAppService
    {
        private readonly ISubCategoryDapperService _subCategoryDapperService;

        public SubCategoryDapperAppService(ISubCategoryDapperService subCategoryDapperService)
        {
            _subCategoryDapperService = subCategoryDapperService;
        }

        public Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
            => _subCategoryDapperService.GetAllAsync(cancellationToken);
    }
}
