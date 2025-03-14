using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Enums;


namespace Achareh.Domain.AppServices
{
    public class RequestAppService : IRequestAppService
    {
        private readonly IRequestService _requestService;

        public RequestAppService(IRequestService requestService)
        {
            _requestService = requestService; 
        }
        public async Task<bool> ChangeStatus(int status, int orderId, CancellationToken cancellationToken)

            => await _requestService.ChangeStatus(status, orderId, cancellationToken);
        

        public async Task<bool> CreateAsync(Request request, CancellationToken cancellationToken)

            => await _requestService.CreateAsync(request, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _requestService.DeleteAsync(id, cancellationToken);


        public async Task<List<Request>> GetAllAsync(CancellationToken cancellationToken)

            => await _requestService.GetAllAsync(cancellationToken);

        public async Task<Request> GetByIdAsync(int Id, CancellationToken cancellationToken)

            => await _requestService.GetByIdAsync(Id, cancellationToken);

        public async Task<Request?> GetByIdWithDetailsAsync(int requestId, CancellationToken cancellationToken)

            => await _requestService.GetByIdWithDetailsAsync(requestId, cancellationToken);

        public async Task<List<Request>?> GetCustomerRequestsAsync(int customerId, CancellationToken cancellationToken)

            => await _requestService.GetCustomerRequestsAsync(customerId, cancellationToken);

        public async Task<List<Request>> GetCustomerRequestsWithDetailsAsync(int customerId, CancellationToken cancellationToken)

            => await _requestService.GetCustomerRequestsWithDetailsAsync(customerId, cancellationToken);

        public async Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken)

           => await _requestService.GetRequestByIdAsync(requestId, cancellationToken);

        public async Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken)

           => await _requestService.GetRequestsInfo(cancellationToken);

        public async Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken)

           => await _requestService.UpdateAsync(request, cancellationToken);

        public async Task<List<Request>> GetCustomerRequestAsync(int userId, CancellationToken cancellationToken)

           => await _requestService.GetCustomerRequestAsync(userId, cancellationToken);

        public async Task<bool> SetWinnerForRequest(int offerId,int requestId, CancellationToken cancellationToken)

          => await _requestService.SetWinnerForRequest(offerId, requestId, cancellationToken);

        public async Task<bool> ChangeStatusOfRequest(StatusEnum status, int orderId, CancellationToken cancellationToken)

         => await _requestService.ChangeStatusOfRequest(status, orderId, cancellationToken);

        public async Task<int> GetPaidByCustomerOrderCountAsync(int userId , CancellationToken cancellationToken)

         => await _requestService.GetPaidByCustomerOrderCountAsync(userId,cancellationToken);

        public async Task<List<Request>> GetRequestsByHomeServices(List<int> homeServiceIds, int cityId, CancellationToken cancellationToken)

         => await _requestService.GetRequestsByHomeServices(homeServiceIds, cityId, cancellationToken);
    }
}
