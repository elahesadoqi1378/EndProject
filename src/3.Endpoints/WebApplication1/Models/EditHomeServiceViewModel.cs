﻿using System.ComponentModel.DataAnnotations;

namespace Achareh.Endpoint.MVC.Models
{
    public class EditHomeServiceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
        [StringLength(100, ErrorMessage = "عنوان نباید بیشتر از ۱۰۰ کاراکتر باشد.")]
        public string Title { get; set; }


        [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است.")]
        public int SubCategoryId { get; set; }

        public string? ImagePath { get; set; } = null;


        [Required(ErrorMessage = "وارد کردن قیمت الزامی است.")]
        [Range(0, int.MaxValue, ErrorMessage = "قیمت باید مقدار مثبت باشد.")]
        public int Price { get; set; }


        [StringLength(500, ErrorMessage = "توضیحات نباید بیشتر از ۵۰۰ کاراکتر باشد.")]
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }


    }
}
