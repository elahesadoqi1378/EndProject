using Achareh.Domain.Core.Entities.BaseEntities;
using Achareh.Domain.Core.Entities.Request;
using Microsoft.AspNetCore.Identity;

namespace Achareh.Domain.Core.Entities.User
{
    public class User : IdentityUser<int>
    {
        #region Properties
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Address { get; set; }
        public double Inventory { get; set; }
        public int CityId { get; set; }
        public string? ImagePath { get; set; }
        public Expert? Expert  { get; set; }
        public Customer? Customer { get; set; }
        public Admin? Admin { get; set; }
       
        #endregion


        #region NavigationProperties
        public City City { get; set; }
        #endregion
    }

}
