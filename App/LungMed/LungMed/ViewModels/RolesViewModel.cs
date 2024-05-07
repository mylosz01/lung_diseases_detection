using System.ComponentModel.DataAnnotations;

namespace LungMed.ViewModels
{
    public class RolesViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    } 
}
