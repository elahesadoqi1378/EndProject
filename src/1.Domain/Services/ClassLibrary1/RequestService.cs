

using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Core.Entities.BaseEntities;
using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Enums;
using System.Threading;

namespace Achareh.Domain.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;

        }

        public async Task<bool> ChangeStatus(int status, int orderId, CancellationToken cancellationToken)

            => await _requestRepository.ChangeStatus(status, orderId, cancellationToken);

        public async Task<bool> CreateAsync(Request request, CancellationToken cancellationToken)

            => await _requestRepository.CreateAsync(request, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

            => await _requestRepository.DeleteAsync(id, cancellationToken);

        public async Task<List<Request>> GetAllAsync(CancellationToken cancellationToken)

            => await _requestRepository.GetAllAsync(cancellationToken);

        public async Task<Request> GetByIdAsync(int Id, CancellationToken cancellationToken)

            => await _requestRepository.GetByIdAsync(Id, cancellationToken);

        public async Task<Request?> GetByIdWithDetailsAsync(int requestId, CancellationToken cancellationToken)

           => await _requestRepository.GetByIdWithDetailsAsync(requestId, cancellationToken);

        public async Task<List<Request>?> GetCustomerRequestsAsync(int customerId, CancellationToken cancellationToken)

           => await _requestRepository.GetCustomerRequestsAsync(customerId, cancellationToken);

        public async Task<List<Request>> GetCustomerRequestsWithDetailsAsync(int customerId, CancellationToken cancellationToken)

          => await _requestRepository.GetCustomerRequestsWithDetailsAsync(customerId, cancellationToken);

        public async Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken)

         => await _requestRepository.GetRequestByIdAsync(requestId, cancellationToken);

        public async Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken)

            => await _requestRepository.GetRequestsInfo(cancellationToken);

        public async Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken)

            => await _requestRepository.UpdateAsync(request, cancellationToken);
        public async Task<List<Request>> GetCustomerRequestAsync(int userId, CancellationToken cancellationToken)

           => await _requestRepository.GetCustomerRequestAsync(userId, cancellationToken);

        public async Task<bool> SetWinnerForRequest(int offerId, int requestId, CancellationToken cancellationToken)

          => await _requestRepository.SetWinnerForRequest(offerId, requestId, cancellationToken);

        public async Task<bool> ChangeStatusOfRequest(StatusEnum status, int orderId, CancellationToken cancellationToken)

          => await _requestRepository.ChangeStatusOfRequest(status, orderId, cancellationToken);

        public async Task<int> GetPaidByCustomerOrderCountAsync(int userId, CancellationToken cancellationToken)

         => await _requestRepository.GetPaidByCustomerOrderCountAsync(userId, cancellationToken);

        public async Task<List<Request>> GetRequestsByHomeServices(List<int> homeServiceIds, int cityId, CancellationToken cancellationToken)

         => await _requestRepository.GetRequestsByHomeServices(homeServiceIds,cityId, cancellationToken);

        public async Task<int?> GetWinnerExpertIdAsync(int requestId, CancellationToken cancellationToken)

        => await _requestRepository.GetWinnerExpertIdAsync(requestId,cancellationToken); 
    }
}
