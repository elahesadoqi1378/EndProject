using System.ComponentModel.DataAnnotations;

namespace Achareh.Endpoint.MVC.Models
{
    public class UpdateCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان الزامی است.")]
        public string Title { get; set; }
        public string? ImagePath { get; set; } = null;
        public IFormFile? ImageFile { get; set; }

    }
}
