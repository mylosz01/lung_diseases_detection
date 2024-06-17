using LungMed.Models;

namespace LungMed.ViewModels
{
    public class RecordingViewModel
    {
        public List<Recording>? Recordings { get; set; }
        public string? LastNameSearch { get; set; }
        public string? PersonalNumberSearch { get; set; }
        public string? sortOrder { get; set; }
    }
}
