using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Dtos.HomeService;
using Achareh.Domain.Core.Dtos.SubCategory;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;


namespace Achareh.Infrastructure.EfCore.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _context;

        public SubCategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)

            => await _context.SubCategories
                            .Include(x => x.HomeServices)
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
                return true;
            }
            catch
            {
                throw new Exception("something is wrong in create");
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
                return true;
            }
            catch
            {
                throw new Exception("something wrong in update");
            }


        }

        public async Task<bool> SubCategoryUpdate(UpdateSubCategoryDto updateSubCategoryDto, CancellationToken cancellationToken)
        {
            var existingModel = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == updateSubCategoryDto.Id, cancellationToken);

            existingModel.Title = updateSubCategoryDto.Title;
            existingModel.ImagePath = updateSubCategoryDto.ImagePath;
            existingModel.CategoryId = updateSubCategoryDto.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        //public async Task<bool> SubCategoryCreate(CreasteSubCategoryDto creasteSubCategoryDto, CancellationToken cancellationToken)
        //{
        //    var newModel = new SubCategory()
        //    {
        //        Title = creasteSubCategoryDto.Title,
        //        ImagePath = creasteSubCategoryDto.ImagePath,
        //        CategoryId = creasteSubCategoryDto.CategoryId
                
        //    };
        //    await _context.SubCategories.AddAsync(newModel, cancellationToken);

        //    await _context.SaveChangesAsync(cancellationToken);

        //    return true;

        //}

        public async Task<bool> DeleteAsync(int id , CancellationToken cancellationToken)
        {
            var subCategory = await _context.SubCategories
                                      .Include(x => x.HomeServices)
                                      .FirstOrDefaultAsync(x => x.Id == id ,  cancellationToken);

            if (subCategory == null)
                return false;

            _context.SubCategories.Remove(subCategory);
            return await _context.SaveChangesAsync(cancellationToken) > 0;

        }

        public async Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken)

            => await _context.SubCategories.ToListAsync(cancellationToken);

    }
}

   

