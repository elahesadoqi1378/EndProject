﻿using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.User;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ExpertRepository> _logger;
        public ExpertRepository(AppDbContext context, ILogger<ExpertRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Experts.AddAsync(expert, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("expert Added Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in create expert", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var expert = await _context.Experts.Include(x => x.User)
                                              .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (expert == null)
                    return false;

                _context.Experts.Remove(expert);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("expert deleted Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in delete expert", ex.Message);
                return false;
            }

        }
        

        public async Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken)

         => await _context.Experts.Include(x=>x.User)
                                   .ToListAsync(cancellationToken);

        public async Task<Expert?> GetByIdAsync(int id, CancellationToken cancellationToken)

          => await _context.Experts.Include(x=>x.User).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        public async Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken)
        {
            try
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

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("expert updated Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in update expert", ex.Message);
                return false;
            }

        }
        public async Task<int> GetCount(CancellationToken cancellationToken)

           => await _context.Experts.AsNoTracking()
                                    .CountAsync(cancellationToken);
    }
    }

