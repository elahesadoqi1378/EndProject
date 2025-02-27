

using Achareh.Domain.Core.Dtos.Category;
using Achareh.Domain.Core.Dtos.SubCategory;
using Achareh.Domain.Core.Entities.Request;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface ISubCategoryService
    {
        Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken);
        Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task<bool> SubCategoryUpdate(UpdateSubCategoryDto updateSubCategoryDto, CancellationToken cancellationToken);
        //Task<bool> SubCategoryCreate(CreasteSubCategoryDto creasteSubCategoryDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
      
    }
}
