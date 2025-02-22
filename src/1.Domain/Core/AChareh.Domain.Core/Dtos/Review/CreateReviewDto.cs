using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Dtos.Review
{
    public class CreateReviewDto
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int CustomerId { get; set; }
        public int ExpertId { get; set; }

    }
}
