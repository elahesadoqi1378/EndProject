using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class HomeServiceRepository : IHomeServiceRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeServiceRepository> _logger;
        private readonly IMemoryCache _memoryCache;

        public HomeServiceRepository(AppDbContext context, ILogger<HomeServiceRepository> logger, IMemoryCache memoryCache)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
        {
            var homeServices = _memoryCache.Get<List<HomeService>>("GetAllAsync");

            if (homeServices is null)
            {
                homeServices = await _context.HomeServices
                .Include(x => x.SubCategory)
                .Where(x => x.IsDeleted == false)
                .ToListAsync(cancellationToken);

            }
            _memoryCache.Set("GetAllAsync", homeServices, TimeSpan.FromMinutes(1));

            return homeServices;

        }


        public async Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.HomeServices
                .Include(x => x.SubCategory)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> CreateAsync(HomeService homeService, CancellationToken cancellationToken)
        {
            try
            {
                await _context.HomeServices.AddAsync(homeService, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("homeservice Added Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in create homeservice", ex.Message);
                return false;
            }


        }

        public async Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken)
        {
            try
            {
                var existingHomeService = await _context.HomeServices
                    .FirstOrDefaultAsync(x => x.Id == homeService.Id, cancellationToken);
                if (existingHomeService == null)
                    return false;

                existingHomeService.Title = homeService.Title;
                existingHomeService.SubCategoryId = homeService.SubCategoryId;
                existingHomeService.VisitCount = homeService.VisitCount;
                existingHomeService.ImagePath = homeService.ImagePath;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("homeservice update Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in update homeservice", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var homeService = await _context.HomeServices
               .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (homeService == null)
                    return false;

                homeService.IsDeleted = true;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("homeservice update Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in delete homeservice", ex.Message);
                return false;
            }

        }
        public async Task<List<HomeService>> GetAllWithSubCategoryId(int subCategoryId, CancellationToken cancellationToken)

          => await _context.HomeServices
                           .Include(x=>x.SubCategory)
                           .ThenInclude(x=>x.Category)
                           .Where(x => x.SubCategoryId == subCategoryId)
                           .ToListAsync(cancellationToken);
    }
}
