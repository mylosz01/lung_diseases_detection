using LungMed.Models;

namespace LungMed.ViewModels
{
    public class HealthCardViewModel
    {
        public List<HealthCard>? HealthCards { get; set; }
        public string? LastNameSearch { get; set; }
        public string? PersonalNumberSearch { get; set; }
        public string? sortOrder { get; set;}
    }
}
