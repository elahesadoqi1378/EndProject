using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Services
{
    public class ExpertOfferService : IExpertOfferService
    {
        private readonly IExpertOfferRepository _expertOfferRepository;

        public ExpertOfferService(IExpertOfferRepository expertOfferRepository)
        {
            _expertOfferRepository = expertOfferRepository;
        }

        public async Task CreateAsync(ExpertOffer expertOffer , CancellationToken cancellationToken)

            => await _expertOfferRepository.CreateAsync(expertOffer,cancellationToken);
        

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)

            => await _expertOfferRepository.DeleteAsync(id,cancellationToken);

        public async Task<List<ExpertOffer>> GetAllAsync(CancellationToken cancellationToken)

            => await _expertOfferRepository.GetAllAsync(cancellationToken);

        public async Task<ExpertOffer> GetByIdAsync(int id , CancellationToken cancellationToken)

            => await _expertOfferRepository.GetByIdAsync(id ,cancellationToken);

        public async Task<List<ExpertOffer>> OffersOfRequest(int requestId, CancellationToken cancellationToken)

            => await _expertOfferRepository.OffersOfRequest(requestId,cancellationToken);

        public async Task UpdateAsync(ExpertOffer expertOffer , CancellationToken cancellationToken)

            => await _expertOfferRepository.UpdateAsync(expertOffer,cancellationToken);


    }
}
