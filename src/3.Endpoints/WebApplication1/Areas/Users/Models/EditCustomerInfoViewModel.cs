﻿using System.ComponentModel.DataAnnotations;

namespace Achareh.Endpoint.MVC.Areas.Users.Models
{
    public class EditCustomerInfoViewModel
    {
       
            [Display(Name = "نام")]
            [Required(ErrorMessage = "فیلد نام اجباری است")]
            public string FirstName { get; set; }

            [Display(Name = "نام خانوادگی")]
            [Required(ErrorMessage = " فیلد نام خانوادگی اجباری است")]
            public string LastName { get; set; }

            [Display(Name = "نام کاربری")]
            [Required(ErrorMessage = "نام کاربری اجباری است")]
            public string UserName { get; set; }

            [Display(Name = "آدرس")]
            [Required(ErrorMessage = " وارد کردن آدرس اجباری است")]
            public string Address { get; set; }

            [Display(Name = "استان")]
            [Required(ErrorMessage = "استان اجباری است")]
            public int CityId { get; set; }

            [Display(Name = "ایمیل")]
            [Required(ErrorMessage = "ایمیل اجباری است")]
            public string Email { get; set; }

            [Display(Name = "شماره تماس")]
            [Required(ErrorMessage = "شماره تماس اجباری است")]
            [StringLength(11, ErrorMessage = "طول شماره تلفن کمتر از 11 عدد است")]
            [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن اشتباه است")]
            public string PhoneNumber { get; set; }

            [Display(Name = "تصویر پروفایل")]
            [Required(ErrorMessage = "تصویر پروفایل اجباری است")]
            public IFormFile? ImageFile { get; set; }

            public string? ImagePath { get; set; }

            [Display(Name = " موجودی")]
            public double Price { get; set; }

       
    }
}
