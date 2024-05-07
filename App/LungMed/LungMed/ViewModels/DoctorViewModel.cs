using LungMed.Models;

namespace LungMed.ViewModels
{
    public class DoctorViewModel
    {
        public List<Doctor>? Doctors { get; set; }
        public string? FirstNameSearch { get; set; }
        public string? LastNameSearch { get; set; }
    }
}
