
namespace Achareh.Domain.Core.Entities.Request
{
    public class SubCategory
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string? ImagePath { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        #endregion

        #region NavigationProperties
        public Category Category { get; set; }
        public List<HomeService>? HomeServices { get; set; }
        #endregion
    }
}
