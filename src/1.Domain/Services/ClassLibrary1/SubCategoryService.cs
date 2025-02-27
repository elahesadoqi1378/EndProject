using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Dtos.Category;
using Achareh.Domain.Core.Dtos.SubCategory;
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        

        public async Task<bool> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken)

            => await _subCategoryRepository.CreateAsync(subCategory, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _subCategoryRepository.DeleteAsync(id, cancellationToken);

        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)

           => await _subCategoryRepository.GetAllAsync(cancellationToken);

        public async Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken)

          => await _subCategoryRepository.GetAllSubCategoriesAsync(cancellationToken);

        public async Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken)

          => await _subCategoryRepository.GetByIdAsync(id, cancellationToken);

        //public async Task<bool> SubCategoryCreate(CreasteSubCategoryDto creasteSubCategoryDto, CancellationToken cancellationToken)


        //  => await _subCategoryRepository.SubCategoryCreate(creasteSubCategoryDto, cancellationToken);

        public async Task<bool> SubCategoryUpdate(UpdateSubCategoryDto updateSubCategoryDto, CancellationToken cancellationToken)

           => await _subCategoryRepository.SubCategoryUpdate(updateSubCategoryDto, cancellationToken);

        public async Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken)

          => await _subCategoryRepository.UpdateAsync(subCategory, cancellationToken);
    }
}
