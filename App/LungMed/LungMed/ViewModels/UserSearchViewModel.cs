using LungMed.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LungMed.ViewModels
{
    public class UserSearchViewModel
    {
        public List<ApplicationUser>? Users { get; set; }
        public SelectList? Roles { get; set; }
        public string? RoleSearchString { get; set; }
        public string? LastNameSearchString { get; set; }
    }
}
