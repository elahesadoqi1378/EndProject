using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان الزامی است.")]
        public string Title { get; set; }
        public string? ImagePath { get; set; } = null;
        public IFormFile? ImageFile { get; set; }
    }

}
