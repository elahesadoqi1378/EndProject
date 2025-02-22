using Achareh.Domain.Core.Entities.Request;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface IExpertOfferService
    {
        Task<List<ExpertOffer>> GetAllAsync(CancellationToken cancellationToken);
        Task<ExpertOffer> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateAsync(ExpertOffer expertOffer, CancellationToken cancellationToken);
        Task UpdateAsync(ExpertOffer expertOffer, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<ExpertOffer>> OffersOfRequest(int requestId, CancellationToken cancellationToken);
    }
}
