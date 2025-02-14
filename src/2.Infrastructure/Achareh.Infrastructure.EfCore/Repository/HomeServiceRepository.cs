using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class HomeServiceRepository : IHomeServiceRepository
    {
        private readonly AppDbContext _context;

        public HomeServiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<HomeService>> GetAllAsync()

               => await _context.HomeServices
                                .Include(x => x.SubCategory)
                                .ToListAsync();

        public async Task<HomeService> GetByIdAsync(int id)

               => await _context.HomeServices
                             .Include(x => x.SubCategory)
                             .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> CreateAsync(HomeService homeService)
        {
            try
            {
                await _context.HomeServices.AddAsync(homeService);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("something is wrong in create");
            }
            
        }
        public async Task<bool> UpdateAsync(HomeService homeService)
        {
            try
            {
                var existingHomeService = await _context.HomeServices.FirstOrDefaultAsync(x => x.Id == homeService.Id);
                if (existingHomeService == null)
                    return false;

                existingHomeService.Title = homeService.Title;
                existingHomeService.SubCategoryId = homeService.SubCategoryId;
                existingHomeService.VisitCount = homeService.VisitCount;
                existingHomeService.ImagePath = homeService.ImagePath;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("something is wrong in update");
            }
            
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var homeService = await _context.HomeServices.FirstOrDefaultAsync(x => x.Id == id);
                if (homeService == null)
                    return false;

                    _context.HomeServices.Remove(homeService);
                    homeService.IsDeleted = true;
                    await _context.SaveChangesAsync();
                    return true;

            }
            catch
            {
                throw new Exception("something is wrong in delete");
            }
           
            
        }

        
    }
}
