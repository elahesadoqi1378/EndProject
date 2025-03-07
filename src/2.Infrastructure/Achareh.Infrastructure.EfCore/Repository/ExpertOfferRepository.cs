
using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Enums;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class ExpertOfferRepository : IExpertOfferRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ExpertOfferRepository> _logger;
        public ExpertOfferRepository(AppDbContext context, ILogger<ExpertOfferRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<ExpertOffer>> GetAllAsync(CancellationToken cancellationToken)

            => await _context.ExpertOffers.ToListAsync(cancellationToken);
        public async Task<ExpertOffer?> GetByIdAsync(int id , CancellationToken cancellationToken)

            => await _context.ExpertOffers
                              .Include(x=>x.Request)
                              .ThenInclude(x=>x.HomeService)
                              .FirstOrDefaultAsync(x => x.Id == id , cancellationToken);

        public async Task CreateAsync(ExpertOffer expertOffer , CancellationToken cancellationToken)
        {

            await _context.ExpertOffers.AddAsync(expertOffer ,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("expertoffer Added Succesfully");

        }
        public async Task UpdateAsync(ExpertOffer expertOffer,CancellationToken cancellationToken)
        {
            var existingOffer = await _context.ExpertOffers.FirstOrDefaultAsync(x => x.Id == expertOffer.Id);
            if (existingOffer != null)
            {
                existingOffer.SuggestedPrice = expertOffer.SuggestedPrice;
                existingOffer.OfferDate = expertOffer.OfferDate;
                existingOffer.Description = expertOffer.Description;
                await _context.SaveChangesAsync();
                _logger.LogInformation("expertoffer updated Succesfully");
            }
        }
        public async Task DeleteAsync(int id,CancellationToken cancellationToken)
        {
            var expertOffer = await _context.ExpertOffers.FirstOrDefaultAsync(x => x.Id == id);
            if (expertOffer != null)
            {
                _context.ExpertOffers.Remove(expertOffer);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("expertoffer deleted Succesfully");
            }
        }

        public async Task<List<ExpertOffer>> OffersOfRequest(int requestId, CancellationToken cancellationToken)

           => await _context.ExpertOffers
                            .Where(r => r.Request.Id == requestId)
                            .Include(c => c.Request)
                            .Include(c => c.Expert) 
                            .ThenInclude(c => c.User)
                            .ToListAsync(cancellationToken);

        public async Task<bool> ChangeStausOfExpertOffer(int offerId,StatusEnum status , CancellationToken cancellationToken)
        {
            try
            {
                var existingExpertOffer = await _context.ExpertOffers.FirstOrDefaultAsync(x => x.Id == offerId, cancellationToken);

                if (existingExpertOffer == null)
                    return false;

                existingExpertOffer.OfferStatusEnum = status;

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("expertoffers status changed Succesfully");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in changing status of expertoffer", ex.Message);
                return false;
            }
        }
            

    }
}
