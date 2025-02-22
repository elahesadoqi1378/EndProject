
using Achareh.Domain.Core.Entities;
using Achareh.Domain.Core.Entities.User;

namespace Achareh.Domain.Core.Entities.Request
{
    public class HomeService
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public int SubCategoryId { get; set; }
        public string Description { get; set; }
        public int VisitCount { get; set; }
        public string? ImagePath { get; set; }
        public int Price { get; set; } //base price
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        #endregion

        #region NavigationProperties
        public SubCategory SubCategory { get; set; }
        public List<Request>? Requests { get; set; }
        public List<Expert>? Experts { get; set; }

        //public List<ExpertHomeService> ExpertHomeServices { get; set; }

        #endregion
    }
}
