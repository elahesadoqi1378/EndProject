﻿using Achareh.Domain.Core.Entities.BaseEntities;
using Achareh.Domain.Core.Entities.User;
using Achareh.Domain.Core.Enums;

namespace Achareh.Domain.Core.Entities.Request
{
    public class Request
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RequestForTime { get; set; }  //tarikhi k mikhay ejra sh
        public DateTime CreatedAt { get; set; } = DateTime.Now; //tarikhi k in darkhast sabt shode
        public bool IsDeleted { get; set; } = false;
        public DateTime? ApprovedAt { get; set; } // tarikh tahvil tarikhi k anajm shode kar
        public StatusEnum RequestStatus { get; set; } = StatusEnum.WatingExpertOffer;
        public int WinnerId { get; set; }
        public int? ReviewId { get; set; }
        public int CityId { get; set; }
        public int CustomerId { get; set; }
        public bool IsReviewd { get; set; } = false;
        public int HomeServiceId { get; set; }

        #endregion

        #region NavigationProperties

        public Review? Review { get; set; }
        public HomeService HomeService { get; set; }
        public City City { get; set; }
        public Customer Customer { get; set; }
        public List<string>? RequestImages { get; set; }
        public List<ExpertOffer>? ExpertOffers { get; set; }
      
        #endregion

    }
}
