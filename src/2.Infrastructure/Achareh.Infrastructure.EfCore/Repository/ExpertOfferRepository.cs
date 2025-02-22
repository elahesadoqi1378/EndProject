
using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class ExpertOfferRepository : IExpertOfferRepository
    {
        private readonly AppDbContext _context;
        public ExpertOfferRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ExpertOffer>> GetAllAsync(CancellationToken cancellationToken)

            => await _context.ExpertOffers.ToListAsync(cancellationToken);
        public async Task<ExpertOffer> GetByIdAsync(int id , CancellationToken cancellationToken)

            => await _context.ExpertOffers.FirstOrDefaultAsync(x => x.Id == id , cancellationToken);

        public async Task CreateAsync(ExpertOffer expertOffer , CancellationToken cancellationToken)
        {
            await _context.ExpertOffers.AddAsync(expertOffer ,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
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
            }
        }
        public async Task DeleteAsync(int id,CancellationToken cancellationToken)
        {
            var expertOffer = await _context.ExpertOffers.FirstOrDefaultAsync(x => x.Id == id);
            if (expertOffer != null)
            {
                _context.ExpertOffers.Remove(expertOffer);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<ExpertOffer>> OffersOfRequest(int requestId, CancellationToken cancellationToken)

           => await _context.ExpertOffers
                            .Include(c => c.Request)
                            .Include(c => c.Expert) 
                            .ThenInclude(c => c.User)
                            .Where(r => r.Request.Id == requestId)
                            .ToListAsync(cancellationToken);
    }
}
