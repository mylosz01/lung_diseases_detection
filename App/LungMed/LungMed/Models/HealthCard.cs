using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LungMed.Validators;

namespace LungMed.Models
{
    public class HealthCard
    {
        public int Id { get; set; }
        public string? Medicines { get; set; }
        public string? Diseases { get; set; }
        public string? Allergies { get; set; }
        [Display(Name = "Bleeding disorders")]
        public string? BleedingDisorders { get; set; }
        public bool? Pregnancy { get; set; }
        [Display(Name = "Pregnancy week")]
        public int? PregnancyWeek { get; set; }
        [DataType(DataType.Date)]
        [DateBeforeTodayAttribute]
        public DateTime Date { get; set; }
        [ForeignKey("Patient")]
        [Required]
        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
