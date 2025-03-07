using Achareh.Domain.Core.Entities.User;
using Achareh.Domain.Core.Enums;

namespace Achareh.Domain.Core.Entities.Request
{
    public class ExpertOffer //pishnahad
    {
        #region Properties
        public int Id { get; set; }
        public int SuggestedPrice { get; set; }
        public DateTime OfferDate { get; set; }  //tarikh tahvil ya tarikhi k mikhay tahvil bdi
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public StatusEnum OfferStatusEnum  { get; set; }
        public string? Description { get; set; }
        public int RequestId { get; set; }
        public int ExpertId { get; set; }
        #endregion

        #region NavigationProperties
        public Request Request { get; set; }
        public Expert Expert { get; set; }

        #endregion
    }
}
