using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Entities.User
{
    public class Customer
    {
        public int Id { get; set; }
        public List<Request.Request> Requests { get; set; }
        public List<Review>? Reviews { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
