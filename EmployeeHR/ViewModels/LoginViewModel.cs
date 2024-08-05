using System.ComponentModel.DataAnnotations;

namespace EmployeeHR.ViewModels
{
    public class LoginViewModel
    {
       
        [Required] 
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
