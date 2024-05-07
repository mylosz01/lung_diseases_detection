using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LungMed.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string FullNameWithId => $"Id: {Id} - {FirstName} {LastName}";
    }
}
