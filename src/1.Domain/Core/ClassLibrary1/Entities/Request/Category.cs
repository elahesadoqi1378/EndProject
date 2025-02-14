
namespace Achareh.Domain.Core.Entities.Request
{
    public class Category
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        #endregion Properties

        #region NavigationProperties
        public List<SubCategory>? SubCategories { get; set; }
        #endregion
    }
}
