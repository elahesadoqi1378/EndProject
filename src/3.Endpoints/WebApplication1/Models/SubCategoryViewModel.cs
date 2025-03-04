using System.ComponentModel.DataAnnotations;

namespace Achareh.Endpoint.MVC.Models
{
    public class SubCategoryViewModel   //create
    {
        [Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
        [StringLength(100, ErrorMessage = "عنوان نباید بیشتر از ۱۰۰ کاراکتر باشد.")]
        public string Title { get; set; }


        [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است.")]
        public int CategoryId { get; set; }


        //[Required(ErrorMessage = "انتخاب تصویر الزامی است.")]
        public string? ImagePath { get; set; } = null;

        public IFormFile? ImageFile { get; set; }
    }

 
}
