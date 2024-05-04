using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LungMed.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirhtDate { get; set; }
        public Sex Sex { get; set; }
        [ForeignKey("Doctor")]
        [Required]
        [Display(Name = "Doctor Id")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public string FullNameWithIdAndPersonal => $"Id: {Id} - {FirstName} {LastName} {PersonalNumber}";
    }
    public enum Sex
    {
        Male,
        Female
    }
}
