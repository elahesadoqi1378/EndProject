using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.User;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
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
            try
            {
                var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Id == id);
                                           
                if (admin == null)
                    return false;

                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken)
        
            =>  await _context.Admins.ToListAsync(cancellationToken);
        

        public async Task<Admin?> GetByIdAsync(int id, CancellationToken cancellationToken)
        
            => await _context.Admins.Include(x => x.User)
                                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        

        public async Task<bool> UpdateAsync(Admin admin, CancellationToken cancellationToken)
        {
            try
            {
                var existAdmin = await _context.Admins.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == admin.Id, cancellationToken);
                if (existAdmin == null)
                   

                return false;

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
            catch
            {

                throw new Exception("Something is wrong check details");
               
            }
           

            
        }
    }
}
