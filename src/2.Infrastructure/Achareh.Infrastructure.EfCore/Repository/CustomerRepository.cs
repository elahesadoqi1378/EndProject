
using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.User;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(AppDbContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Customers.AddAsync(customer, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("customer Added Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in create customer", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (customer == null)
                    return false;

                _context.Customers.Remove(customer);
               
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("customer deleted Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in delete customer", ex.Message);
                return false;
            }


        }

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
        
            => await _context.Customers.Include(x=>x.User)
                                       .ToListAsync(cancellationToken);
        

        public async Task<Customer?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Customers
             .Include(x => x.Requests)
             .ThenInclude(x => x.ExpertOffers)
             .Include(x => x.Reviews)
             .Include(x => x.User)
             .ThenInclude(x => x.City)
             .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);
        }

     


        public async Task<Customer?> GetrByIdAsync(int id, CancellationToken cancellationToken)
        
            => await _context.Customers.Include(x=>x.User)
                                       .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)

           => await _context.Customers.Include(x => x.User)
                                      .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);


        public async Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken)
        {
            try
            {
                var existingCustomer = await _context.Customers
                                                     .Include(x => x.User)
                                                     .FirstOrDefaultAsync(x => x.Id == customer.Id, cancellationToken);

                if (existingCustomer == null)
                    return false;

                existingCustomer.User.FirstName = customer.User.FirstName;
                existingCustomer.User.LastName = customer.User.LastName;
                existingCustomer.User.Email = customer.User.Email;
                existingCustomer.User.PhoneNumber = customer.User.PhoneNumber;
                existingCustomer.User.Address = customer.User.Address;
                existingCustomer.User.CityId = customer.User.CityId;
                existingCustomer.User.ImagePath = customer.User.ImagePath;
                 await _context.SaveChangesAsync(cancellationToken) ;
                _logger.LogInformation("customer updated Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in update customer", ex.Message);
                return false;
            }


        }
        public async Task<int> GetCount(CancellationToken cancellationToken) 

            => await  _context.Customers.AsNoTracking()
                                        .CountAsync(cancellationToken);
    }
}
