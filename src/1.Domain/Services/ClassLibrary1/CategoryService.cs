
using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Dtos.Category;
using Achareh.Domain.Core.Entities.Request;

namespace Achareh.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepositroy _categoryRepositroy;

        public CategoryService(ICategoryRepositroy categoryRepositroy)
        {
            _categoryRepositroy = categoryRepositroy;
        }

        public async Task<bool> CategoryCreate(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)

             => await _categoryRepositroy.CategoryCreate(createCategoryDto, cancellationToken);

        public async Task<bool> CategoryUpdate(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken)

             => await _categoryRepositroy.CategoryUpdate(updateCategoryDto, cancellationToken);

        public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken)

            => await _categoryRepositroy.CreateAsync(category, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _categoryRepositroy.DeleteAsync(id, cancellationToken);

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)

            => await _categoryRepositroy.GetAllAsync(cancellationToken);

        public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)

            => await _categoryRepositroy.GetAllCategoriesAsync(cancellationToken);

        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _categoryRepositroy.GetByIdAsync(id, cancellationToken);

        public async Task<Category> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

            => await _categoryRepositroy.GetByIdWithDetailsAsync(id, cancellationToken);

        public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)

           => await _categoryRepositroy.UpdateAsync(category, cancellationToken);
    }
}
