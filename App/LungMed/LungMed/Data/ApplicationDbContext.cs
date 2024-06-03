using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LungMed.Models;

namespace LungMed.Data
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LungMed.Models.Doctor> Doctor { get; set; } = default!;
        public DbSet<LungMed.Models.Patient> Patient { get; set; } = default!;
        public DbSet<LungMed.Models.HealthCard> HealthCard { get; set; } = default!;
        public DbSet<LungMed.Models.Recording> Recording { get; set; } = default!;

    }
}
