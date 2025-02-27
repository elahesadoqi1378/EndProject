using Achareh.Domain.Core.Enums;

namespace Achareh.Endpoint.MVC.Models
{

    public class RequestStatusViewModel
    {
        public int RequestId { get; set; }
        public StatusEnum CurrentStatus { get; set; }
        public StatusEnum NewStatus { get; set; } 
        public List<StatusEnum> AvailableStatuses { get; set; } = new List<StatusEnum>(); 
    }


}
