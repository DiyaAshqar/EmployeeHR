using System.ComponentModel.DataAnnotations;

namespace EmployeeHR.ViewModels
{
    public class ManageUserViewModel
    { 
        public string Username { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
