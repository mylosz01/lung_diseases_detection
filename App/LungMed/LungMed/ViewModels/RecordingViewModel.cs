using LungMed.Models;

namespace LungMed.ViewModels
{
    public class RecordingViewModel
    {
        public List<Recording>? Recordings { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
    }
}
