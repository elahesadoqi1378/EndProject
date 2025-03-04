using System.ComponentModel.DataAnnotations;

namespace Achareh.Endpoint.MVC.Models
{
    public class AdminLoginViewModel
    {
        
            [Display(Name = "ایمیل")]
            [Required(ErrorMessage = "فیلد ایمیل کاربری اجباری است ")]
            public string Email { get; set; }

            [Display(Name = "رمز عبور")]
            [Required(ErrorMessage = "فیلد ایمیل اجباری است")]
            [StringLength(6, ErrorMessage = "طول رمز عبور اشتباه است")]
            public string Password { get; set; }
        
    }
}

