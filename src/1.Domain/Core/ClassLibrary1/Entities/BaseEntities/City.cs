using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;

namespace Achareh.Domain.Core.Entities.BaseEntities
{
    public class City
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        #endregion

        #region NavigationProperties
        public List<User.User>? Users { get; set; }
        public List<Request.Request>? Requests { get; set; }
        #endregion
    }
}
