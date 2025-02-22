using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Repositroy
{
    public interface IExpertOfferRepository
    {
        Task<List<ExpertOffer>> GetAllAsync(CancellationToken cancellationToken);
        Task<ExpertOffer> GetByIdAsync(int id , CancellationToken cancellationToken);
        Task CreateAsync(ExpertOffer expertOffer , CancellationToken cancellationToken);
        Task UpdateAsync(ExpertOffer expertOffer , CancellationToken cancellationToken);
        Task DeleteAsync(int id,CancellationToken cancellationToken);
        Task<List<ExpertOffer>> OffersOfRequest(int requestId, CancellationToken cancellationToken);
    }
}
