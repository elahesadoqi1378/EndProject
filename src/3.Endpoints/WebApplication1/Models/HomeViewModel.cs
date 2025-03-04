using Achareh.Domain.Core.Entities.Request;

namespace Achareh.Endpoint.MVC.Models
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}
