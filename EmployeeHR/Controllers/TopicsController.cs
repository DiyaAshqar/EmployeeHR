using EmployeeHR.Common;
using EmployeeHR.Data;
using EmployeeHR.Models;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EmployeeHR.Controllers
{
    public class TopicsController : Controller
    {
        private readonly HRDbContext _dbContext;

        public TopicsController(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [ActionFilter]
        public IActionResult GetDepartments()
        {
            var departmentList = _dbContext.Departments
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Abbreviation)
                .Select(x => x.Name).Distinct()
                .AsNoTracking()
                .ToList();
            return View(departmentList);
        }

        public JsonResult GetPayroll()//example for OrderBy and ThenBy
        {
            var list = _dbContext.Payrolls.Include(x => x.Employee).OrderBy(x => x.EmployeeId)
                .ThenBy(x => x.PayrollDate).ToList();
            return Json(JsonConvert.SerializeObject(list));
        }

        public List<string> SkipAndTake([FromQuery] int pageNo = 0, int pagesize = 0)
        {
            //var result = _dbContext.Departments.Skip(2).Take(3).ToList();
            var result = _dbContext.Departments.Skip((pageNo - 1) * pagesize).Take(pagesize).ToList();
            return result.Select(x => x.Name).ToList();
        }

        ////Basic return types of actionresults in ASP.NET MVC are :-
        //ViewResult
        //PartialViewResult
        //Contentresult
        //Emptyresult
        //Fileresult
        //Json result
        //Javascript result

        public ViewResult Index()
        {
            return View();
        }
        public PartialViewResult Index1()
        {
            return PartialView();
        }
        public ContentResult Index2()
        {
            return Content("<h1><font color='blue'>I AM HAPPY WITH<font>, WHAT I HAVE<h1><script>alert('HELLO THERE , HOW R U?');<script>");
        }
        public EmptyResult Index3()
        {
            return new EmptyResult();
        }
        public FileResult Index4()
        {
            return File("~/wwwroot/images/PhotoPic.png", "text/plain","fileDownloadName");
        }
        public JsonResult Index5()
        {
            return Json(new { WEBSITE = "WWW.USMTECHWORLD.COM", NOOFVISITORS = "2,00,000" });
            //,JsonRequestBehavior.AllowGet
        }

        //get list result as genteric
        public ActionResult<List<DepartmentModel>> GetDepartmentsList()
        {

            return View(new List<DepartmentModel>());
        }

    }
}
