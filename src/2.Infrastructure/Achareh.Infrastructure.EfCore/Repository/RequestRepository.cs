using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Achareh.Domain.Core.Enums;


namespace Achareh.Infrastructure.EfCore.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _context;

        public RequestRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Request>> GetAllAsync(CancellationToken cancellationToken)

            => await _context.Requests.ToListAsync(cancellationToken);

        public async Task<Request> GetByIdAsync(int id)

            => await _context.Requests.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Request request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Request request)
        {
            var existingRequest = await _context.Requests.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingRequest != null)
            {
                existingRequest.Title = request.Title;
                existingRequest.Description = request.Description;
                existingRequest.CreatedAt = request.CreatedAt;
                existingRequest.CreatedAt = request.CreatedAt;
                existingRequest.ApprovedAt = request.ApprovedAt;
                existingRequest.RequestStatus = request.RequestStatus;
                existingRequest.CityId = request.CityId;
                existingRequest.CustomerId = request.CustomerId;
                existingRequest.HomeServiceId = request.HomeServiceId;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(x => x.Id == id);
            if (request != null)
            {
                await _context.SaveChangesAsync();
            }
        }



        public async Task<Request> GetByIdAsync(int Id, CancellationToken cancellationToken)

         => await _context.Requests.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);


        public async Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken)

            => await _context.Requests.FirstOrDefaultAsync(x => x.Id == requestId, cancellationToken);
        public async Task<List<Request>?> GetCustomerRequestsAsync(int customerId, CancellationToken cancellationToken)

            => await _context.Requests.Where(x => x.CustomerId == customerId).ToListAsync(cancellationToken);

        public async Task<List<Request>> GetWithDetailsAsync(int requestId, CancellationToken cancellationToken)

           => await _context
                    .Requests
                    .Include(r => r.HomeService)
                    .Include(r => r.City)
                    .Include(r => r.Images)
                    .Include(r => r.ExpertOffers)
                    .ToListAsync(cancellationToken);



        public async Task<List<Request>> GetCustomerRequestsWithDetailsAsync(int customerId, CancellationToken cancellationToken)

             => await _context
               .Requests
               .Where(r => r.CustomerId == customerId) //eager loading select
               .Include(r => r.HomeService)
               .Include(r => r.City)
               .Include(r => r.Images)
               .Include(r => r.ExpertOffers)
               .ToListAsync(cancellationToken);


        public async Task<Request?> GetByIdWithDetailsAsync(int requestId, CancellationToken cancellationToken)

             => await _context
                   .Requests
                   .Include(r => r.HomeService)
                   .Include(r => r.City)
                   .Include(r => r.Images)
                   .Include(r => r.ExpertOffers)
                   .FirstOrDefaultAsync(e => e.Id == requestId, cancellationToken);

        public async Task<bool> CreateAsync(Request request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Requests.AddAsync(request, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken)
        {
            var existRequest = await _context.Requests.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existRequest == null)
                return false;

            existRequest.Title = request.Title;
            existRequest.Description = request.Description;
            existRequest.RequestForTime = request.RequestForTime;
            existRequest.ApprovedAt = request.ApprovedAt;
            existRequest.RequestStatus = request.RequestStatus;
            existRequest.CityId = request.CityId;

            return await _context.SaveChangesAsync(cancellationToken) > 0;

        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var request = await _context.Requests
                                 .Include(x => x.Images)
                                 .Include(x => x.ExpertOffers)
                                 .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (request == null)
                return false;

            request.IsDeleted = true;

            return await _context.SaveChangesAsync(cancellationToken) > 0;

        }

        public async Task<bool> ChangeStatus(int status, int orderId, CancellationToken cancellationToken)
        {
            var existingRequest = await _context.Requests.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);

            if (existingRequest == null)
                return false; 

            existingRequest.RequestStatus = (StatusEnum)status; 

            await _context.SaveChangesAsync(cancellationToken); 
            return true;
        }
        public async Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken)

             => await _context.Requests
            .Include(c => c.Customer)
            .ThenInclude(c => c.User)
            .Include(h => h.HomeService)
            .ToListAsync(cancellationToken);

       
    }
}
