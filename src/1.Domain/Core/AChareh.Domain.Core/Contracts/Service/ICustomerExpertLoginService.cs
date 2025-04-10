using Achareh.Domain.Core.Entities.User;
using AChareh.Domain.Core.Dtos.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AChareh.Domain.Core.Contracts.Service
{
    public interface ICustomerExpertLoginService
    {
        Task<SignInResult> Login(LoginUserDto loginUserDto);
        Task Logout();
        Task<IdentityResult> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken);
    }
}
