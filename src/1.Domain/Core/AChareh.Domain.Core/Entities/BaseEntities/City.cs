﻿using Achareh.Domain.Core.Entities.Request;
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
        public List<User.User>? Users { get; set; } = new List<User.User>();
        public List<Request.Request>? Requests { get; set; } = new List<Request.Request>();
        #endregion
    }
}
