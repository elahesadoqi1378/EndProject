using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.BaseEntities;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
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

        public CityRepository(AppDbContext context)
        {
            _context = context;
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
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (city == null)
                return false;

            _context.Cities.Remove(city);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(City city, CancellationToken cancellationToken)
        {
            var existingCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == city.Id, cancellationToken);

            if (existingCity == null)
                return false;

            existingCity.Title = city.Title;
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
