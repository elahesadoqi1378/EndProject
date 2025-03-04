using System.ComponentModel.DataAnnotations;

namespace Achareh.Endpoint.MVC.Models
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "عنوان الزامی است.")]
        public string Title { get; set; }
        public string? ImagePath { get; set; } = null;
        public IFormFile? ImageFile { get; set; }

    }
}
