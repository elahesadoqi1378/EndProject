using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.AppServices
{
    public class ExpertOfferAppService : IExpertOfferAppService
    {
        private readonly IExpertOfferService _expertOfferService;

        public ExpertOfferAppService(IExpertOfferService expertOfferService)
        {
            _expertOfferService = expertOfferService;
        }

       
        public async Task CreateAsync(ExpertOffer expertOffer, CancellationToken cancellationToken)

           => await _expertOfferService.CreateAsync(expertOffer, cancellationToken);

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)

           => await _expertOfferService.DeleteAsync(id, cancellationToken);

        public async Task<List<ExpertOffer>> GetAllAsync(CancellationToken cancellationToken)

            => await _expertOfferService.GetAllAsync(cancellationToken);

        public async Task<ExpertOffer> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _expertOfferService.GetByIdAsync(id, cancellationToken);

        public async Task<List<ExpertOffer>> OffersOfRequest(int requestId, CancellationToken cancellationToken)

            => await _expertOfferService.OffersOfRequest(requestId, cancellationToken);

        public async Task UpdateAsync(ExpertOffer expertOffer, CancellationToken cancellationToken)

            => await _expertOfferService.UpdateAsync(expertOffer, cancellationToken);

        public async Task<bool> ChangeStausOfExpertOffer(int offerId, StatusEnum status, CancellationToken cancellationToken)

           => await _expertOfferService.ChangeStausOfExpertOffer(offerId, status, cancellationToken);
    }
}
