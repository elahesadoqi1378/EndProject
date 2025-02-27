using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Dtos.HomeService;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class HomeServiceRepository : IHomeServiceRepository
    {
        private readonly AppDbContext _context;

        public HomeServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.HomeServices
                .Include(x => x.SubCategory)
                .ToListAsync(cancellationToken);
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
                return true;
            }
            catch
            {
                throw new Exception("something is wrong in create");
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
                return true;
            }
            catch
            {
                throw new Exception("something is wrong in update");
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var homeService = await _context.HomeServices
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (homeService == null)
                return false;

            _context.HomeServices.Remove(homeService);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> HomeServiceUpdate(UpdateHomeServiceDto updateHomeServiceDto, CancellationToken cancellationToken)
        {
            var existingModel = await _context.HomeServices.FirstOrDefaultAsync(x => x.Id == updateHomeServiceDto.Id, cancellationToken);
            
            existingModel.Title = updateHomeServiceDto.Title;
            existingModel.Description = updateHomeServiceDto.Description;
            existingModel.ImagePath = updateHomeServiceDto.ImagePath;
            existingModel.Price = updateHomeServiceDto.Price;
            existingModel.SubCategoryId = updateHomeServiceDto.SubCategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> HomeServiceCreate(CreateHomeServiceDto createHomeServiceDto, CancellationToken cancellationToken)
        {
            var newModel = new HomeService()
            {
                Title = createHomeServiceDto.Title,
                ImagePath = createHomeServiceDto.ImagePath,
                Price = createHomeServiceDto.Price,
                SubCategoryId = createHomeServiceDto.SubCategoryId,
                Description = createHomeServiceDto.Description
            };
            await _context.HomeServices.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
