using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;


namespace Achareh.Infrastructure.EfCore.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SubCategoryRepository> _logger;

        public SubCategoryRepository(AppDbContext context, ILogger<SubCategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)

            => await _context.SubCategories.Where(x=>x.IsDeleted == false)
                                           .ToListAsync(cancellationToken);

        public async Task<SubCategory> GetByIdAsync(int id , CancellationToken cancellationToken)

            => await _context.SubCategories
                             .Include(x => x.HomeServices)
                             .FirstOrDefaultAsync(x => x.Id == id , cancellationToken);

        public async Task<bool> CreateAsync(SubCategory subCategory , CancellationToken cancellationToken)
        {
            try
            {
                await _context.SubCategories.AddAsync(subCategory,cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("subcategory created Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in create subcategory", ex.Message);
                return false;
            }

        }
        public async Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            try
            {
                var existingSubCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == subCategory.Id,cancellationToken);

                if (existingSubCategory == null)
                    return false;

                existingSubCategory.Title = subCategory.Title;
                existingSubCategory.CategoryId = subCategory.CategoryId;
                existingSubCategory.ImagePath = subCategory.ImagePath;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("subcategory updated Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in update subcategory", ex.Message);
                return false;
            }


        }

     
        public async Task<bool> DeleteAsync(int id , CancellationToken cancellationToken)
        {
            try
            {
                var subCategory = await _context.SubCategories
                                                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (subCategory == null)
                    return false;

                subCategory.IsDeleted = true;
                _logger.LogInformation("subcategory deleted Succesfully");
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in delete subcategory", ex.Message);
                return false;
            }


        }

        public async Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken)

            => await _context.SubCategories.ToListAsync(cancellationToken);

      
    }
}

   

