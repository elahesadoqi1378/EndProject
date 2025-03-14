using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.User;
using Achareh.Infrastructure.EfCore.Common;
using AChareh.Domain.Core.Dtos.User;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        public ExpertRepository(AppDbContext context, ILogger<ExpertRepository> logger, UserManager<User> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
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

         => await _context.Experts.Include(x => x.User)
                                   .ToListAsync(cancellationToken);

        public async Task<Expert?> GetByIdAsync(int id, CancellationToken cancellationToken)

          => await _context.Experts.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);



        public async Task<Expert?> GetExpertByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

            => await _context
            .Experts
            .Include(x => x.User)
            .ThenInclude(x => x.City)
            .Include(x => x.HomeServices)
            .ThenInclude(x => x.SubCategory)
            .ThenInclude(x => x.Category)
            .Include(x => x.ExpertOffers)
            .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);


        public async Task<EditExpertDto?> GetExpertProfileByIdAsync(int id, CancellationToken cancellationToken)
        {
            //var expert = await _appDbContext
            // .Experts
            // .Include(e => e.User)
            // .ThenInclude(c => c.City)
            // .Include(e => e.HomeServices)
            // .ThenInclude(e => e.SubCategory)
            // .ThenInclude(e => e.Category)
            // .Include(e => e.Suggestions)
            // .FirstOrDefaultAsync(e => e.UserId == id, cancellationToken);

            //if (expert == null)
            // return null;
            var expert = await _context.Experts
            .Where(e => e.UserId == id)
            .Select(e => new EditExpertDto
            {
                Id = e.Id,
                FirstName = e.User.FirstName,
                LastName = e.User.LastName,
                UserName = e.User.UserName,
                Email = e.User.Email,
                PhoneNumber = e.User.PhoneNumber,
                Address = e.User.Address,
                PicturePath = e.User.ImagePath,
                CityTitle = e.User.City.Title,
                Balance = e.User.Inventory,
                RegisterDate = e.User.CreatedAt,
                HomeServices = e.HomeServices.Select(hs => new ServiceDto
                {
                    Id = hs.Id,
                    Title = hs.SubCategory.Title,

                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

            return expert;
        }



        public async Task<bool> UpdateAsync(Expert expert, List<int> selectedHomeServiceIds, CancellationToken cancellationToken)
        {
            try
            {
                if (expert == null || expert.UserId == null)
                {
                    _logger.LogError("Expert or Expert.UserId is null.");
                    return false;
                }

                var existExpert = await _context.Experts
                    .Include(x => x.User)
                    .Include(x => x.HomeServices)
                    .FirstOrDefaultAsync(x => x.UserId == expert.UserId, cancellationToken);

                if (existExpert == null)
                {
                    _logger.LogWarning($"Expert with UserId {expert.UserId} not found.");
                    return false;
                }

                existExpert.User.Address = expert.User.Address;
                existExpert.User.FirstName = expert.User.FirstName;
                existExpert.User.LastName = expert.User.LastName;
                existExpert.User.UserName = expert.User.UserName;
                existExpert.User.Email = expert.User.Email;
                existExpert.User.CityId = expert.User.CityId;
                existExpert.User.ImagePath = expert.User.ImagePath;
                existExpert.User.PhoneNumber = expert.User.PhoneNumber;

                if (selectedHomeServiceIds != null)
                {
                    if (existExpert.HomeServices != null)
                    {
                        existExpert.HomeServices.RemoveAll(hs => !selectedHomeServiceIds.Contains(hs.Id));

                        var newHomeServices = await _context.HomeServices
                            .Where(hs => selectedHomeServiceIds.Contains(hs.Id))
                            .ToListAsync(cancellationToken);

                        foreach (var newHomeService in newHomeServices)
                        {
                            if (!existExpert.HomeServices.Any(hs => hs.Id == newHomeService.Id))
                            {
                                existExpert.HomeServices.Add(newHomeService);
                            }
                        }
                    }
                    else
                    {
                        _logger.LogWarning("existExpert.HomeServices is null.");
                    }

                }
                else if (existExpert.HomeServices != null)
                {
                    existExpert.HomeServices.Clear();
                }

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Expert updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating expert: {ex.Message}");
                return false;
            }
        }
        public async Task<int> GetCount(CancellationToken cancellationToken)

           => await _context.Experts.AsNoTracking()
                                    .CountAsync(cancellationToken);


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

