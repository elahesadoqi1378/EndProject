using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Repositroy
{
    public interface IRequestRepository
    {
        Task<List<Request>> GetAllAsync(CancellationToken cancellationToken);
        Task<Request> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken);
        Task<List<Request>?> GetCustomerRequestsAsync(int customerId, CancellationToken cancellationToken);
        Task<List<Request>> GetCustomerRequestsWithDetailsAsync(int customerId, CancellationToken cancellationToken);
        Task<Request?> GetByIdWithDetailsAsync(int requestId, CancellationToken cancellationToken);
        Task<bool> ChangeStatus(int status, int orderId, CancellationToken cancellationToken);
        Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken);
        Task<bool> CreateAsync(Request request,CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Request request,CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id,CancellationToken cancellationToken);
        Task<List<Request>> GetCustomerRequestAsync(int userId, CancellationToken cancellationToken);
        Task<bool> SetWinnerForRequest(int offerId, int requestId, CancellationToken cancellationToken);
        Task<bool> ChangeStatusOfRequest(StatusEnum status, int orderId, CancellationToken cancellationToken);
        Task<int> GetPaidByCustomerOrderCountAsync(int userId , CancellationToken cancellationToken);
    }
}
