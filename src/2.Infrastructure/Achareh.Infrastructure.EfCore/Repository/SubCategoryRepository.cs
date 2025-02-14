using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;


namespace Achareh.Infrastructure.EfCore.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _context;

        public SubCategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<SubCategory>> GetAllAsync()

            => await _context.SubCategories
                            .Include(x => x.HomeServices)
                            .ToListAsync();

        public async Task<SubCategory> GetByIdAsync(int id)

            => await _context.SubCategories
                             .Include(x => x.HomeServices)
                             .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> CreateAsync(SubCategory subCategory)
        {
            try
            {
                await _context.SubCategories.AddAsync(subCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("something is wrong in create");
            }

        }
        public async Task<bool> UpdateAsync(SubCategory subCategory)
        {
            try
            {
                var existingSubCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == subCategory.Id);

                if (existingSubCategory != null)
                    return false;

                existingSubCategory.Title = subCategory.Title;
                existingSubCategory.CategoryId = subCategory.CategoryId;
                existingSubCategory.ImagePath = subCategory.ImagePath;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("something wrong in update");
            }


        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var subCategory = await _context.SubCategories
                                       .Include(x => x.HomeServices)
                                       .FirstOrDefaultAsync(x => x.Id == id);

                if (subCategory == null)
                    return false;

                _context.SubCategories.Remove(subCategory);
                subCategory.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("something wrong in delete");
            }

        }
    }
}

   

