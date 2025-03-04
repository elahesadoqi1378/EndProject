using Achareh.Domain.Core.Entities.BaseEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Achareh.Endpoint.MVC.Models
{
    public class CreateUserViewModel
    {

        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [MaxLength(50, ErrorMessage = "نام نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [MaxLength(50, ErrorMessage = "نام خانوادگی نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید")]
        [MinLength(6, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا تکرار رمز عبور را وارد کنید")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "لطفا یک شهر انتخاب کنید")]
        public int CityId { get; set; }

        public string? ImagePath { get; set; } = null;

        public IFormFile? ImageFile { get; set; }
    }
   
}
