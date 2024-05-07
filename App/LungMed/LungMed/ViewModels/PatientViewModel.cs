using LungMed.Models;
using System.ComponentModel.DataAnnotations;

namespace LungMed.ViewModels
{
    public class PatientViewModel
    {
        public List<Patient>? Patients { get; set; }
        public string? PersonalNumberPatientSearch { get; set; }
        public string? LastNameDoctorSearch { get; set; }
        public int? DoctorIdSearch { get; set; }

    }
}
