using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Dtos.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "عنوان الزامی است.")]
        public string Title { get; set; }
    }
}
