using EmployeeHR.Data;
using EmployeeHR.Models;
using EmployeeHR.ViewModels;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NuGet.Frameworks;
using NuGet.Packaging.Signing;

namespace EmployeeHR.Controllers
{

    public class PayrollController : Controller
    {
        private readonly HRDbContext _dbContext;
        public PayrollController(HRDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //var model = _dbContext.Payrolls.Include(x => x.Employee).Select(x =>new PayrollModel { } ).ToList();
             
            var viewModel = _dbContext.Payrolls.Include(x => x.Employee).Select(x => new PayrollViewModel
            {
                Id = x.Id,
                PayrollDate = x.PayrollDate,
                Bonus = x.Bonus,
                SocialSecurityAmount = x.SocialSecurityAmount,
                CreatedBy = x.CreatedBy,
                EmployeeFullName = $"{x.Employee.FirstName} {x.Employee.LastName}",
                NetSalary = x.NetSalary
            }).ToList();


            //var model= (from p in _dbContext.Payrolls join )
            return View(viewModel);
        }

        public ActionResult Create()
        {

            ViewBag.EmployeeList = _dbContext.Employees.Select(x => new
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName
            }
            ).ToList();

            //ViewBag.EmployeeList = _dbContext.Employees.Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.LastName }).ToList();
            //ViewBag.EmployeeList = (from emp in _dbContext.Employees
            //                        select new
            //                        {
            //                            Id = emp.Id,
            //                            Name = emp.FirstName + " " + emp.LastName
            //                        });


            return View();
        }

        [HttpPost]
        public ActionResult Create(PayrollViewModel payrollViewModel)
        {
            if (payrollViewModel != null)
            {
                var netSalary = SalaryCalculation(payrollViewModel);
                if (netSalary == 0)
                {
                    return View();
                }
                PayrollModel payroll = new PayrollModel()
                {
                    NetSalary = netSalary,
                    PayrollDate = payrollViewModel.PayrollDate,
                    EmployeeId = payrollViewModel.EmployeeId,
                    Bonus = payrollViewModel.Bonus,
                    SocialSecurityAmount = payrollViewModel.SocialSecurityAmount,
                    Leaves = payrollViewModel.Leaves,
                    TS = DateTime.Now,
                    CreatedBy = "Logged User"
                };

                _dbContext.Payrolls.Add(payroll);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            ViewBag.EmployeeList = _dbContext.Employees.Select(x => new
            { Id = x.Id, Name = x.FirstName + " " + x.LastName }).ToList();
            //var model = _dbContext.Payrolls.FirstOrDefault(x => x.Id == id);
            var viewModel = _dbContext.Payrolls.Include(x => x.Employee).Select(x => new PayrollViewModel
            {
                Id = x.Id,
                PayrollDate = x.PayrollDate,
                Bonus = x.Bonus,
                SocialSecurityAmount = x.SocialSecurityAmount,
                CreatedBy = x.CreatedBy,
                EmployeeFullName = $"{x.Employee.FirstName} {x.Employee.LastName}",
                NetSalary = x.NetSalary,
                Leaves = x.Leaves,
                BasicSalary = x.Employee.BasicSalary,
                EmployeeId = x.EmployeeId
            }).FirstOrDefault(x => x.Id == id);
            return View("Create", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, PayrollViewModel payroll)
        {
            var model = _dbContext.Payrolls.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                model.EmployeeId = payroll.EmployeeId;
                model.PayrollDate = payroll.PayrollDate;
                model.Bonus = payroll.Bonus;
                model.Leaves = payroll.Leaves;
                model.SocialSecurityAmount = payroll.SocialSecurityAmount;
                model.TS = DateTime.Now;
                model.NetSalary = SalaryCalculation(payroll);
            }

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        public ActionResult Delete(int id)
        {
            var model = _dbContext.Payrolls.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                _dbContext.Payrolls.Remove(model);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }


        private decimal SalaryCalculation(PayrollViewModel payroll)
        {
            decimal netSalary = 0;

            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == payroll.EmployeeId);
            //////////Tax Calculation
            if (employee != null)
            {
                var leavsAmount = (employee?.BasicSalary / 30 / 8) * Convert.ToDecimal(payroll.Leaves);
                netSalary = employee.BasicSalary + payroll.Bonus - payroll.SocialSecurityAmount - Convert.ToDecimal(leavsAmount);
            }
            else
            {
                netSalary = 0;
            }

            return netSalary;
        }

        [HttpGet]
        public IActionResult GetBasicSalary(int employeeId)
        {
            try
            {
                var basicSalary = _dbContext.Employees.FirstOrDefault(x => x.Id == employeeId).BasicSalary;
                return Ok(basicSalary);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }







        //[HttpGet]
        //public IActionResult GetBasicSalary(int id)
        //{
        //    decimal basicSalary = _dbContext.Employees.FirstOrDefault(x => x.Id == id).BasicSalary;
        //    return Ok(basicSalary);
        //}
    }
}
