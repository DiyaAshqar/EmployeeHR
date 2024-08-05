﻿namespace EmployeeHR.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string  UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
