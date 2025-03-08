using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.User;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ExpertRepository> _logger;
        private readonly UserManager<User> _userManager;

        public AdminRepository(AppDbContext context, ILogger<ExpertRepository> logger, UserManager<User> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<bool> CreateAsync(Admin admin, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Admins.AddAsync(admin, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Id == id);

            if (admin == null)
            {
                return false;

            }
            else
            {
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync(cancellationToken);
                return true;

            }

        }

        public async Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken)

            => await _context.Admins.ToListAsync(cancellationToken);


        public async Task<Admin?> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _context.Admins.Include(x => x.User)
                                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);



        public async Task<bool> UpdateAsync(Admin admin, CancellationToken cancellationToken)
        {

            var existAdmin = await _context.Admins.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == admin.Id, cancellationToken);

            if (existAdmin == null)
            {
                return false;
            }
            else
            {
                existAdmin.User.Address = admin.User.Address;
                existAdmin.User.FirstName = admin.User.FirstName;
                existAdmin.User.LastName = admin.User.LastName;
                existAdmin.User.Email = admin.User.Email;
                existAdmin.User.CityId = admin.User.CityId;
                existAdmin.User.ImagePath = admin.User.ImagePath;
                existAdmin.User.PhoneNumber = admin.User.PhoneNumber;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }

        }

        public async Task<bool> InventoryIncreaseAsync(string userId, double amount, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {

                    return false;
                }


                user.Inventory += amount;


                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {

                    return true;
                }
                else
                {

                    foreach (var error in result.Errors)
                    {
                        _logger.LogError($"Error updating user inventory: {error.Code} - {error.Description}");
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while increasing user inventory.");
                return false;
            }
        }
    }
}
