using Achareh.Domain.Core.Entities.User;
using AChareh.Domain.Core.Contracts.Service;
using AChareh.Domain.Core.Dtos.User;
using Microsoft.AspNetCore.Identity;


namespace Achareh.Domain.Services
{
    public class CustomerExpertLoginService  : ICustomerExpertLoginService
    {


        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CustomerExpertLoginService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<SignInResult> Login(LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);

            if (user == null && !await _userManager.IsInRoleAsync(user, loginUserDto.Role))
            {
                return SignInResult.Failed;
            }

            return await _signInManager.PasswordSignInAsync(user,loginUserDto.Password, loginUserDto.IsPresistent, false);

        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

        }

    }
}




