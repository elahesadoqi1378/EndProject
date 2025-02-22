using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Service
{
    public interface IRequestService
    {
        Task<List<Request>> GetAllAsync(CancellationToken cancellationToken);
        Task<Request> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken);
        Task<List<Request>?> GetCustomerRequestsAsync(int customerId, CancellationToken cancellationToken);
        Task<List<Request>> GetCustomerRequestsWithDetailsAsync(int customerId, CancellationToken cancellationToken);
        Task<Request?> GetByIdWithDetailsAsync(int requestId, CancellationToken cancellationToken);
        Task<bool> ChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken);
        Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken);
        Task<bool> CreateAsync(Request request, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

    }
}
