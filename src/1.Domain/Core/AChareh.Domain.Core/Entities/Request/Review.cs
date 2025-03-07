using Achareh.Domain.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Entities.Request
{
    public class Review  //nazarat va emtiazat
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }  // 1 ta 5 
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsAccept { get; set; } = false;
        public int RequestId { get; set; }
        public int CustomerId { get; set; }
        public int ExpertId { get; set; }
        #endregion

        #region NavigationProperties

        public Request Request { get; set; }
        public Customer Customer{ get; set; }
        public Expert Expert { get; set; }
        #endregion


    }
}
