using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Achareh.Domain.Services
{
    public class AdminService : IAdminService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAdminRepository _adminRepository;

        public AdminService(SignInManager<User> signInManager, UserManager<User> userManager, IAdminRepository adminRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _adminRepository = adminRepository;
        }

        public Task<bool> InventoryIncreaseAsync(string userId, double amount, CancellationToken cancellationToken)
        {
            return _adminRepository.InventoryIncreaseAsync(userId, amount, cancellationToken);
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return SignInResult.Failed;
            }

           
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return SignInResult.Failed;
            }

          
            return await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
      
    }
}
//var result = await _signInManager.PasswordSignInAsync(userName, pass, false, false);
//if (result.Succeeded)
//{
//    // پیدا کردن یوزر با استفاده از نام کاربری
//    var user = await _userManager.FindByNameAsync(userName);

//    // چک کردن رول
//    if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
//    {
//        return result; // همه‌چیز اوکیه
//    }

//    // اگر رول Admin نبود، کاربر رو لاگ‌اوت کن
//    await _signInManager.SignOutAsync();
//    return SignInResult.Failed; // چون رولش Admin نیست
//}
//return result;