using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.User;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly AppDbContext _context;
        public ExpertRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Experts.AddAsync(expert, cancellationToken);
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
            var expert = await _context.Experts.Include(x => x.User)
                                               .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (expert == null)
                return false;

            _context.Experts.Remove(expert);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        

        public async Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken)

         => await _context.Experts.Include(x=>x.User)
                                   .ToListAsync(cancellationToken);

        public async Task<Expert?> GetByIdAsync(int id, CancellationToken cancellationToken)

          => await _context.Experts.Include(x=>x.User).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        public async Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken)
        {
            var existExpert = await _context.Experts
                                      .Include(x => x.User)
                                      .FirstOrDefaultAsync(c => c.Id == expert.Id, cancellationToken);

            if (existExpert == null)
                return false;

            existExpert.User.Address = expert.User.Address;
            existExpert.User.FirstName = expert.User.FirstName;
            existExpert.User.LastName = expert.User.LastName;
            existExpert.User.Email = expert.User.Email;
            existExpert.User.CityId = expert.User.CityId;
            existExpert.User.ImagePath = expert.User.ImagePath;
            existExpert.User.PhoneNumber = expert.User.PhoneNumber;

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<int> GetCount(CancellationToken cancellationToken)

           => await _context.Experts.AsNoTracking()
                                    .CountAsync(cancellationToken);
    }
    }

