using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AChareh.Domain.Core.Dtos.User
{
    public class EditExpertDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string CityTitle { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double Balance { get; set; }
        public DateTime RegisterDate { get; set; }

        public List<ServiceDto> HomeServices { get; set; } = new();
        public List<SuggestionDto> Suggestions { get; set; } = new();
        public List<RatingDto> Ratings { get; set; } = new();

     
    }
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    
    }
    public class SuggestionDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class RatingDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
