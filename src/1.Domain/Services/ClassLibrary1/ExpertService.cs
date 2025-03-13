using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.User;
using AChareh.Domain.Core.Dtos.User;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public ExpertService(IExpertRepository expertRepository, UserManager<User> userManager)
        {
            _expertRepository = expertRepository;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken)

            => await _expertRepository.CreateAsync(expert, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _expertRepository.DeleteAsync(id, cancellationToken);

        public async Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken)

            => await _expertRepository.GetAllAsync(cancellationToken);

        public async Task<int> GetCount(CancellationToken cancellationToken)

            => await _expertRepository.GetCount(cancellationToken);

        public async Task<Expert?> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _expertRepository.GetByIdAsync(id, cancellationToken);

        public async Task<Expert?> GetExpertByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

           => await _expertRepository.GetExpertByIdWithDetailsAsync(id, cancellationToken);

        public async Task<bool> UpdateAsync(Expert expert, List<int> selectedHomeServiceIds, CancellationToken cancellationToken)

            => await _expertRepository.UpdateAsync(expert, selectedHomeServiceIds, cancellationToken);

        public Task<IdentityResult> RegisterAsync(User user, string pass)
        {
            return _userManager.CreateAsync(user, pass);
        }
        public Task<IdentityResult> UpdateAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public Task<bool> InventoryIncreaseAsync(string userId, double amount, CancellationToken cancellationToken)
        {
            return _expertRepository.InventoryIncreaseAsync(userId, amount, cancellationToken);
        }

        public Task<EditExpertDto?> GetExpertProfileByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _expertRepository.GetExpertProfileByIdAsync(id, cancellationToken);
        }
    }
}
