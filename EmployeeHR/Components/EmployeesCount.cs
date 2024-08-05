using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using EmployeeHR.Data;
using Microsoft.AspNetCore.Html;

namespace EmployeeHR.Components
{
    public class EmployeesCount :ViewComponent
    {
        private readonly HRDbContext _dbContext;
        public EmployeesCount(HRDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public ContentViewComponentResult Invoke()
        {
            var count = _dbContext.Employees.Count();
            return Content($"No. Of Employees {count}");
        }
        
        /// <summary>
        /// to return without encoded data
        /// </summary>
        /// <returns></returns>
        //public HtmlContentViewComponentResult Invoke()
        //{
        //    return new HtmlContentViewComponentResult(new HtmlString("<h2> No. Of Employees 11 <h2>"));
        //}

        //public IViewComponentResult Invoke()
        //{
        //    return View();
        //}

    }
}
