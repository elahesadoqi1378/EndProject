using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Enums;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface IExpertOfferService
    {
        Task<List<ExpertOffer>> GetAllAsync(CancellationToken cancellationToken);
        Task<ExpertOffer> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(ExpertOffer expertOffer, CancellationToken cancellationToken);
        Task UpdateAsync(ExpertOffer expertOffer, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<ExpertOffer>> OffersOfRequest(int requestId, CancellationToken cancellationToken);
        Task<bool> ChangeStausOfExpertOffer(int offerId, StatusEnum status, CancellationToken cancellationToken);
    }
}
