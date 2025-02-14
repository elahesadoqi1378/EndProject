

using Achareh.Domain.Core.Entities.Request;

namespace Achareh.Domain.Core.Contracts.Repositroy
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategory>> GetAllAsync();
        Task<SubCategory> GetByIdAsync(int id);
        Task<bool> CreateAsync(SubCategory subCategory);
        Task<bool> UpdateAsync(SubCategory subCategory);
        Task<bool> DeleteAsync(int id);
    }
}
