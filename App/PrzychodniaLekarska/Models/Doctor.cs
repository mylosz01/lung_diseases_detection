using System.ComponentModel.DataAnnotations;

namespace PrzychodniaLekarska.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        [Display(Name = "Phone number")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Correct phone number has 9 digits.")]
        public string? PhoneNumber { get; set; }
    }
}
