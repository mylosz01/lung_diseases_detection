using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LungMed.Validators;

namespace LungMed.Models
{
    public class HealthCard
    {
        public int Id { get; set; }
        [Required]
        public string? Medicines { get; set; }
        [Required]
        public string? Diseases { get; set; }
        [Required]
        public string? Allergies { get; set; }
        [Required]
        [Display(Name = "Bleeding disorders")]
        public string? BleedingDisorders { get; set; }
        [Required]
        public bool? Pregnancy { get; set; }
        [Display(Name = "Pregnancy week")]
        public int? PregnancyWeek { get; set; }
        [Display(Name = "Issue Date")]
        public DateTime Date { get; set; } = DateTime.Now;
        [ForeignKey("Patient")]
        [Required]
        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

    }
}
