using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.AppServices
{
    public class AdminAppService : IAdminAppService
    {
        private readonly IAdminService _adminService;

        public AdminAppService(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public Task<bool> InventoryIncreaseAsync(string userId, double amount, CancellationToken cancellationToken)
        {
            return _adminService.InventoryIncreaseAsync(userId, amount, cancellationToken);
        }

        public async Task<SignInResult> LoginAsync(string email, string password)

             => await _adminService.LoginAsync(email, password);


        public async Task LogoutAsync()

            => await _adminService.LogoutAsync();
      
    }
}
