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
    public class SubCategoryDapperService : ISubCategoryDapperService
    {
        private readonly ISubCategoryDapperRepository _subCategoryDapperRepository;

        public SubCategoryDapperService(ISubCategoryDapperRepository subCategoryDapperRepository)
        {
            _subCategoryDapperRepository = subCategoryDapperRepository;
        }

        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
            => await _subCategoryDapperRepository.GetAllAsync(cancellationToken);
    }
}
