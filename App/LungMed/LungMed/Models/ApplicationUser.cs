using Microsoft.AspNetCore.Identity;

namespace LungMed.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? FullName => $"{FirstName} {LastName}";

        public string? RoleId { get; set; }

        public IdentityRole Role { get; set; }

        public int? DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public int? PatientId { get; set; }

        public Patient Patient { get; set; }

    }
}
