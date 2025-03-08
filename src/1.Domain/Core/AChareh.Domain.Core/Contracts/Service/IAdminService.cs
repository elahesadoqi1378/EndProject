using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface IAdminService
    {
       Task<SignInResult> LoginAsync(string email, string password);
       Task LogoutAsync();
       Task<bool> InventoryIncreaseAsync(string userId, double amount, CancellationToken cancellationToken);
    }

}

