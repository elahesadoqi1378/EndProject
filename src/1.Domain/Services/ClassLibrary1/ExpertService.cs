using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Services
{
    public class ExpertService : IExpertService
    {
        private readonly IExpertRepository _expertRepository;

        public ExpertService(IExpertRepository expertRepository)
        {
            _expertRepository = expertRepository;
        }

        public async Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken)

            => await _expertRepository.CreateAsync(expert, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _expertRepository.DeleteAsync(id, cancellationToken);

        public async Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken)

            => await _expertRepository.GetAllAsync( cancellationToken);

        public async Task<int> GetCount(CancellationToken cancellationToken)

            => await _expertRepository.GetCount(cancellationToken);

        public async Task<Expert?> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _expertRepository.GetByIdAsync(id, cancellationToken);

        public async Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken)

            => await _expertRepository.UpdateAsync(expert, cancellationToken);
    }
}
