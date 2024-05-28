using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LungMed.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [DisplayName("Email")]

        public string Email { get; set; }

        [DisplayName("First Name")]

        public string FirstName { get; set; }

        [DisplayName("Last Name")]

        public string LastName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("User Name")]

        public string UserName { get; set; }

        public string? FullName => $"{FirstName} {LastName}";

        [DisplayName("User Role")]

        public string? RoleId { get; set; }

        public string? Doctor { get; set; }

        public string? Patient { get; set; }

        public string PersonalNumber { get; set; }

    }
}
