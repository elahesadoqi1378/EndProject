using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Achareh.Domain.Core.Enums;
using Microsoft.Extensions.Logging;
using Achareh.Domain.Core.Entities.User;
using System.Threading;



namespace Achareh.Infrastructure.EfCore.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RequestRepository> _logger;

        public RequestRepository(AppDbContext context, ILogger<RequestRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<Request>> GetAllAsync(CancellationToken cancellationToken)

            => await _context.Requests.ToListAsync(cancellationToken);

        public async Task<Request> GetByIdAsync(int id)

            => await _context.Requests.FirstOrDefaultAsync(x => x.Id == id);


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
                    .Include(r => r.RequestImages)
                    .Include(r => r.ExpertOffers)
                    .ToListAsync(cancellationToken);



        public async Task<List<Request>> GetCustomerRequestsWithDetailsAsync(int customerId, CancellationToken cancellationToken)

             => await _context
               .Requests
               .Where(r => r.CustomerId == customerId) //eager loading select
               .Include(r => r.HomeService)
               .Include(r => r.City)
               .Include(r => r.RequestImages)
               .Include(r => r.ExpertOffers)
               .ToListAsync(cancellationToken);

       

        public async Task<Request?> GetByIdWithDetailsAsync(int requestId, CancellationToken cancellationToken)

             => await _context
                   .Requests
                   .Include(r => r.HomeService)
                   .Include(r => r.City)
                   .FirstOrDefaultAsync(e => e.Id == requestId, cancellationToken);



        public async Task<bool> CreateAsync(Request request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Requests.AddAsync(request, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("request created Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in create request", ex.Message);
                return false;
            }

        }

        public async Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken)
        {
            try
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

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("request updated Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in update request", ex.Message);
                return false;
            }


        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var request = await _context.Requests
                                .Include(x => x.RequestImages)
                                .Include(x => x.ExpertOffers)
                                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (request == null)
                    return false;

                request.IsDeleted = true;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("request deleted Succesfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in delete request", ex.Message);
                return false;
            }

        }

        public async Task<bool> ChangeStatus(int status, int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var existingRequest = await _context.Requests.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);

                if (existingRequest == null)
                    return false;

                existingRequest.RequestStatus = (StatusEnum)status;

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("requests status changed Succesfully");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in changing status of request", ex.Message);
                return false;
            }

        }

        public async Task<bool> ChangeStatusOfRequest(StatusEnum status, int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var existingRequest = await _context.Requests.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);

                if (existingRequest == null)
                    return false;

                existingRequest.RequestStatus = status;

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("requests status changed Succesfully");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("something is wrong in changing status of request", ex.Message);
                return false;
            }

        }

        public async Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken)

             => await _context.Requests
            .Include(c => c.Customer)
            .ThenInclude(c => c.User)
            .Include(h => h.HomeService)
            .ToListAsync(cancellationToken);
        public async Task<List<Request>> GetCustomerRequestAsync(int userId, CancellationToken cancellationToken)

          => await _context.Requests
                           .Where(r => r.Customer.UserId == userId)
                           .Include(r => r.Customer)
                           .ToListAsync(cancellationToken);

        public async Task<bool> SetWinnerForRequest(int offerId, int requestId, CancellationToken cancellationToken)
        {
            try
            {
                var existingRequest = await _context.Requests.FirstOrDefaultAsync(x => x.Id == requestId, cancellationToken);
                if (existingRequest != null)
                {
                    existingRequest.WinnerId = offerId;
                    await _context.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation("winner request set Succesfully");
                    return true;
                }
                else
                {
                    _logger.LogError($"Request with ID {requestId} not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting winner for request.");
                return false;
            }



        }

        public async Task<int> GetPaidByCustomerOrderCountAsync(int userId,CancellationToken cancellationToken)
        {
            return await _context.Requests
                .Where(x=>x.Customer.UserId == userId)
                .Include(x=>x.Customer)
                .CountAsync(r => r.RequestStatus == StatusEnum.WorkPaidByCustomer , cancellationToken);
        }

        public async Task<List<Request>> GetRequestsByHomeServices(List<int> homeServiceIds,int cityId, CancellationToken cancellationToken)
        {
            return await _context.Requests
                .Where(x => homeServiceIds.Contains(x.HomeServiceId) && x.CityId==cityId && x.RequestStatus == StatusEnum.WatingExpertOffer )
                .ToListAsync(cancellationToken);
        }
        public async Task<int?> GetWinnerExpertIdAsync(int requestId, CancellationToken cancellationToken)
        {
            return await _context.Requests
            .Where(r => r.Id == requestId)
            .Select(r => r.ExpertOffers
            .Where(s => s.Id == r.WinnerId)
            .Select(s => (int?)s.ExpertId)
            .FirstOrDefault())
            .FirstOrDefaultAsync(cancellationToken);
        }

    }

}
