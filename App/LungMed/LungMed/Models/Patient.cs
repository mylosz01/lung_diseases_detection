using LungMed.Validators;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LungMed.Models
{
    [Index(nameof(PersonalNumber), IsUnique = true)]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class Patient
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Personal Number")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Personal number must have 11 digits")]
        public string PersonalNumber { get; set; }
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must have 9 digits")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DateBeforeToday]
        public DateTime BirhtDate { get; set; }
        public Sex Sex { get; set; }
        [ForeignKey("Doctor")]
        [Required]
        [Display(Name = "Doctor Id")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public string FullNameWithIdAndPersonal => $"{FirstName} {LastName} - {PersonalNumber}";

        
    }
    public enum Sex
    {
        Male,
        Female
    }
}
