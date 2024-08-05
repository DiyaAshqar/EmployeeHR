using System.ComponentModel.DataAnnotations;

namespace EmployeeHR.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
     
}


public class User
{
    public  int Id { get; set; }

    public List<UserRoles> UserRoles { get; set; }
}
public class UserRoles
{ 
    public int UserId { get; set; }
    public User user { get; set; }

    public int RoleId { get; set; }
    public Role role { get; set; }

}
public class Role
{
    public  int Id { get; set; }
    public List<UserRoles> UserRoles { get; set; }
}