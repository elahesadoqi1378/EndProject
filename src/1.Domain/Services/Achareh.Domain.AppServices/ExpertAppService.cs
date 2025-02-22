using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.AppServices
{
    public class ExpertAppService : IExpertAppService
    {
        private readonly IExpertService _expertService;

        public ExpertAppService(IExpertService expertService)
        {
            _expertService = expertService;
        }

        public async Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken)

              => await _expertService.CreateAsync(expert,cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

              => await _expertService.DeleteAsync(id, cancellationToken);

        public async Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken)

              => await _expertService.GetAllAsync(cancellationToken);

        public async Task<Expert?> GetByIdAsync(int id, CancellationToken cancellationToken)

              => await _expertService.GetByIdAsync(id, cancellationToken);

        public async Task<int> GetCount(CancellationToken cancellationToken)

             => await _expertService.GetCount(cancellationToken);

        public async Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken)

             => await _expertService.UpdateAsync(expert, cancellationToken);
    }
}
