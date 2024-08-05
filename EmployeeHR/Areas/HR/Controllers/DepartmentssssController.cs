using EmployeeHR.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHR.Areas.HR.Controllers
{
    public class DepartmentssssController : Controller
    {
        private readonly HRDbContext _dbContext;
        public DepartmentssssController(HRDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var departmentList = _dbContext.Departments.ToList();
            return View(departmentList);
        }
    }
}
