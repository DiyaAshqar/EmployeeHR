using System.ComponentModel.DataAnnotations;

namespace EmployeeHR.ViewModels
{
    public class RoleFormViewModel
    {
        [Required]
        [Display(Name = "Role Name"), StringLength(100)]
        public string Name { get; set; }
    }
}
