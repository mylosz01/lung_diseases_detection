using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LungMed.Models
{
    public class Recording
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }

        [ForeignKey("Patient")]
        [Required]
        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
