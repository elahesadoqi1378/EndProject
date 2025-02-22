using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Dtos.SubCategory
{
    public class UpdateSubCategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
        [StringLength(100, ErrorMessage = "عنوان نباید بیشتر از ۱۰۰ کاراکتر باشد.")]
        public string Title { get; set; }


        [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است.")]
        public int CategoryId { get; set; }


        [Required(ErrorMessage = "انتخاب تصویر الزامی است.")]
        public string ImagePath { get; set; }
    }
}
