using EmployeeHR.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeHR.Areas.HR.Models;
using EmployeeHR.ViewModels;
using Microsoft.AspNetCore.Identity;
using EmployeeHR.Models.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace EmployeeHR.Data
{
    public class HRDbContext : IdentityDbContext
    {
        public HRDbContext(DbContextOptions<HRDbContext> options) : base(options)
        {

        }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<PayrollModel> Payrolls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "sec");
            builder.Entity<IdentityRole>().ToTable("Roles", "sec");

            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "sec");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "sec");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "sec");
            builder.Entity<IdentityUser>().ToTable("Users", "sec");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "sec");


        }
        

    }
}
