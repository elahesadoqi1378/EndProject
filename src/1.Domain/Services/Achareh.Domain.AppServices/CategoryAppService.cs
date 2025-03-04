using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Dtos.Category;
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.AppServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;

        public CategoryAppService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken)

            => await _categoryService.CreateAsync(category, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _categoryService.DeleteAsync(id, cancellationToken);

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)

            => await _categoryService.GetAllAsync(cancellationToken);

        public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)

            => await _categoryService.GetAllCategoriesAsync(cancellationToken);

       
        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _categoryService.GetByIdAsync(id, cancellationToken);

        public async Task<Category> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

            => await _categoryService.GetByIdWithDetailsAsync(id, cancellationToken);

        public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)

            => await _categoryService.UpdateAsync(category, cancellationToken);

        public async Task<List<Category>> GetAllWithSubCategoriesAsync(CancellationToken cancellationToken)

           => await _categoryService.GetAllWithSubCategoriesAsync(cancellationToken);


    }
}
