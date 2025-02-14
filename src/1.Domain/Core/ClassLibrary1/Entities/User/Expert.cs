using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Entities.User
{
    public class Expert
    {
        #region Properties
        public int Id { get; set; }
        public int UserId { get; set; }
        #endregion

        #region NavigationProperties

        public List<HomeService>? HomeServices { get; set; } //maharatha
        public List<ExpertOffer>? ExpertOffers { get; set; } //pishnahadha
        public List<Review>? Reviews { get; set; }
        public User User { get; set; }

        #endregion
    }
}
