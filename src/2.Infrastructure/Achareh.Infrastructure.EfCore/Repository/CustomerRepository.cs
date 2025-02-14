
using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.User;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace Achareh.Infrastructure.EfCore.Repository
{
    
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Customers.AddAsync(customer, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch
            {
                throw new Exception("something is wrong in create");
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

                if (customer == null)
                    return false;

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch 
            {
                throw new Exception("something is wrong in delete");
            }
        }

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
        
            => await _context.Customers.ToListAsync(cancellationToken);
        

        public async Task<Customer?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Customers
            .Include(x=>x.User)
            .Include(x => x.Requests)
            .ThenInclude(x => x.ExpertOffers)
            .ThenInclude(x => x.Expert)
            .Include(x => x.Reviews).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Customer?> GetrByIdAsync(int id, CancellationToken cancellationToken)
        
            => await _context.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        

        public async Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken)
        {
            try
            {
                var existingCustomer = await _context.Customers
                                                     .Include(c => c.User)
                                                     .FirstOrDefaultAsync(x => x.Id ==customer.Id , cancellationToken);
                if (existingCustomer == null )
                    return false;

                existingCustomer.User.FirstName = customer.User.FirstName;
                existingCustomer.User.LastName = customer.User.LastName;
                existingCustomer.User.Email = customer.User.Email;
                existingCustomer.User.PhoneNumber = customer.User.PhoneNumber;
                existingCustomer.User.Address = customer.User.Address;
                existingCustomer.User.CityId = customer.User.CityId;
                existingCustomer.User.ImagePath = customer.User.ImagePath;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch 
            {
                throw new Exception("something is wrong in update");
            }

        }
    }
}
