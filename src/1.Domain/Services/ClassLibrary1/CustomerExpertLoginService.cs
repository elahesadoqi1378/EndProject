using Achareh.Domain.Core.Contracts.Service;
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
        private readonly ICustomerService _customerService;
        private readonly IExpertService _expertService;

        public CustomerExpertLoginService(UserManager<User> userManager, SignInManager<User> signInManager, ICustomerService customerService, IExpertService expertService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
            _expertService = expertService;
        }


        public async Task<SignInResult> Login(LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (user != null && await _userManager.IsInRoleAsync(user, loginUserDto.Role))
            {
                return await _signInManager.PasswordSignInAsync(user, loginUserDto.Password, loginUserDto.IsPresistent, false);
            }

            return SignInResult.Failed;

        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

        }
        public async Task<IdentityResult> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken)
        {
            object registerUser;
            if (registerUserDto.Role != "Customer" && registerUserDto.Role != "Expert")
            {
                return IdentityResult.Failed();
            }
            var user = new User
            {
                UserName = registerUserDto.Email,
                CityId = registerUserDto.CityId,

            };
            var result = await _userManager.CreateAsync(user, registerUserDto.Password);
            if (result.Succeeded)
            {
                if (registerUserDto.Role == "Customer")
                {
                    registerUser = new Customer
                    {
                        UserId = user.Id,
                        User = user
                    };
                }
                else
                {
                    registerUser = new Expert
                    {
                        UserId = user.Id,
                        User = user
                    };
                }

                var roleResult = await _userManager.AddToRoleAsync(user, registerUserDto.Role);

                if (roleResult.Succeeded)
                {
                    if (registerUser.GetType() == typeof(Customer))
                    {
                        if (await _customerService.CreateAsync((Customer)registerUser, cancellationToken))
                        {
                            return IdentityResult.Success;
                        }
                        return IdentityResult.Failed();
                    }

                    else if (registerUser.GetType() == typeof(Expert))
                    {
                        if (await _expertService.CreateAsync((Expert)registerUser, cancellationToken))
                        {
                            return IdentityResult.Success;
                        }
                        return IdentityResult.Failed();
                    }


                }




                return IdentityResult.Failed();



            }

            return result;
        }

    }
}




