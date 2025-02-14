

using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class CategoryRepository : ICategoryRepositroy
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)

               => await _context.Categories
                        .ToListAsync(cancellationToken);

        public async Task<Category?> GetByIdAsync(int id , CancellationToken cancellationToken)

                => await _context.Categories
                        .FirstOrDefaultAsync(x => x.Id == id , cancellationToken);

        public async Task<Category?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

            => await _context
                 .Categories

                 .Include(c => c.SubCategories)
                 .ThenInclude(c => c.HomeServices)
                 .FirstOrDefaultAsync(x => x.Id == id ,cancellationToken);
                 

        public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Categories.AddAsync(category, cancellationToken);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("something is wrong in create");
            }
            
        }
        public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            try
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

                if (existingCategory == null)
                    return false;

                existingCategory.Title = category.Title;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("something is wrong in update");
            }
           

        }
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _context.Categories
                                        .Include(x => x.SubCategories)
                                        .ThenInclude(x => x.HomeServices)
                                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (category == null)
                    return false;
                _context.Categories.Remove(category);
                category.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("something is wrong");
            }
 
        }

    }
}
