using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.BaseEntities;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Infrastructure.EfCore.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CityRepository> _logger;

        public CityRepository(AppDbContext context, ILogger<CityRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
        {
            var cities = await _context.Cities.ToListAsync(cancellationToken);
            return cities;

        }

      
        public async Task<bool> CreateAsync(City city, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Cities.AddAsync(city, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("city Added Succesfully");
                return true;
            }
            catch
            {
                _logger.LogError("something is wrong in create city");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (city == null)
                    return false;

                _context.Cities.Remove(city);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("city deleted Succesfully");
                return true;

            }
            catch(Exception ex)
            {
                _logger.LogError("something is wrong in delete city" , ex.Message);
                return false;
            }
           
        }

        public async Task<bool> UpdateAsync(City city, CancellationToken cancellationToken)
        {
            try
            {
                var existingCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == city.Id, cancellationToken);

                if (existingCity == null)
                    return false;

                existingCity.Title = city.Title;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("city updated Succesfully");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in update city", ex.Message);
                return false;
            }

        }
    }
}
