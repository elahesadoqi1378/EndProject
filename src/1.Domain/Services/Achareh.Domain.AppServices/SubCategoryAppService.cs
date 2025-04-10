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
    public class SubCategoryAppService : ISubCategoryAppService
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryAppService(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }
        public async Task<bool> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken)

            => await _subCategoryService.CreateAsync(subCategory, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _subCategoryService.DeleteAsync(id, cancellationToken);

        //public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)

        //     => await _subCategoryService.GetAllAsync(cancellationToken);

        public async Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken)

             => await _subCategoryService.GetAllSubCategoriesAsync(cancellationToken);

        public async Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _subCategoryService.GetByIdAsync(id, cancellationToken);

        public async Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken)

            => await _subCategoryService.UpdateAsync(subCategory, cancellationToken);
    }
}
