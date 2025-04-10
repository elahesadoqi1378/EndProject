

using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Dtos.Category;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class CategoryRepository : ICategoryRepositroy
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;
        private readonly IMemoryCache _memoryCache;
        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger, IMemoryCache memoryCache)
        {
            _context = context;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        //public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)

        //{
        //    var categories = _memoryCache.Get<List<Category>>("GetAllAsync");
        //    if (categories is null)
        //    {
        //        categories = await _context.Categories.ToListAsync(cancellationToken);
        //    }
        //    _memoryCache.Set("GetAllAsync", categories, TimeSpan.FromMinutes(10));

        //    return categories;
        //}

        
              

        public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken)

                => await _context.Categories
                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Category?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

            => await _context
                 .Categories

                 .Include(c => c.SubCategories)
                 .ThenInclude(c => c.HomeServices)
                 .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


        public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Categories.AddAsync(category, cancellationToken);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Category Added Succesfully");
                return true;
            }
            catch
            {
                _logger.LogError("something is wrong in create category");
                return false;
            }

        }
        public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            try
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id, cancellationToken);

                if (existingCategory == null)
                    return false;

                existingCategory.Title = category.Title;
                existingCategory.ImagePath = category.ImagePath;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Category updated Succesfully");
                return true;
         
            }
            catch
            {
                _logger.LogError("something is wrong in update category");
                return false;
            }

        }
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _context.Categories
                                             .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (category == null)
                    return false;

                category.IsDeleted = true;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Category deleted Succesfully");
                return true;
            }
            catch
            {
                _logger.LogError("something is wrong in delete category");
                return false;
            }
          
        }

        public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)

            => await _context.Categories.ToListAsync();


        public async Task<List<Category>> GetAllWithSubCategoriesAsync(CancellationToken cancellationToken)

             => await _context.Categories
                              .Where(c => !c.IsDeleted)
                              .Include(c => c.SubCategories.Where(s => !s.IsDeleted))
                              .ToListAsync(cancellationToken);

    }
}
