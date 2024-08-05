using Microsoft.AspNetCore.Mvc;
using EmployeeHR.Data;
using Microsoft.EntityFrameworkCore.Migrations;
using EmployeeHR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EmployeeHR.Common;
using Newtonsoft.Json;

namespace EmployeeHR.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly HRDbContext _dbContext;
        public DepartmentController(HRDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

         
        [ActionFilter]
        public IActionResult Index()
        {
            var departmentList = _dbContext.Departments
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Abbreviation)
                .Select(x => x.Name).Distinct()
                .AsNoTracking()
                .ToList();
            return View(departmentList);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DepartmentModel department)
        {
            if (department != null)
            {
                _dbContext.Departments.Add(department);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = _dbContext.Departments.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                return View("Create", model);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public ActionResult Edit(int id, DepartmentModel department)
        {
            var model = _dbContext.Departments.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                model.Name = department.Name;
                model.Abbreviation = department.Abbreviation;
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Create", department);
        }


        public ActionResult Delete(int id)
        {
            var model = _dbContext.Departments.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                _dbContext.Departments.Remove(model);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
