
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
        public async Task<List<ExpertOffer>> GetAllAsync()

            => await _context.ExpertOffers.ToListAsync();
        public async Task<ExpertOffer> GetByIdAsync(int id)

            => await _context.ExpertOffers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(ExpertOffer expertOffer)
        {
            await _context.ExpertOffers.AddAsync(expertOffer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ExpertOffer expertOffer)
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
        public async Task DeleteAsync(int id)
        {
            var expertOffer = await _context.ExpertOffers.FirstOrDefaultAsync(x => x.Id == id);
            if (expertOffer != null)
            {
                _context.ExpertOffers.Remove(expertOffer);
                expertOffer.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
