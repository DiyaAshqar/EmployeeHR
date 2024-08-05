using EmployeeHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using EmployeeHR.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore.Query.Internal;
using EmployeeHR.ViewModels;
using Newtonsoft.Json;
namespace EmployeeHR.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly HRDbContext _dbContext;
        Uri _baseUri = new Uri("https://localhost:7191/api");

        public EmployeeController(HRDbContext dbContext)
        {
            this._dbContext = dbContext;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseUri;

        }
       

     
        public IActionResult Index()
        {

            var allPayrolls = _dbContext.Employees.SelectMany(x => x.Payrolls).ToList();
            var deps = _dbContext.Departments.OrderBy(x => x.Name).ThenBy(x => x.Abbreviation);
            var allPayrollsssss = _dbContext.Employees.Select(x => x.Payrolls).ToList();
            if (allPayrolls.Any())
            {

            }

            HttpResponseMessage responseMessage = _httpClient.GetAsync(_baseUri + "/Employee").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = responseMessage.Content.ReadAsStringAsync().Result;

                var empList = JsonConvert.DeserializeObject<List<EmployeeModel>>(response);
                return View(empList);
            }
            return View();
        }


        //public IActionResult GetAll()
        //{
        //    HttpResponseMessage responseMessage = _httpClient.GetAsync(_httpClient.BaseAddress + "/Employee").Result;
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        string data = responseMessage.Content.ReadAsStringAsync().Result;
        //        List<EmployeeModel> employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(data);
        //        return View("Index", employees);
        //    }
        //    return View("Index");
        //}




        //public IActionResult Index(string search)
        //{
        //    try
        //    {
        //        //var employees = (from emp in _dbContext.Employees
        //        //                 join dep in _dbContext.Departments
        //        //                 on emp.DepartmentId equals dep.Id
        //        //                 select new EmployeeModel
        //        //                 {
        //        //                     Id = emp.Id,
        //        //                     FirstName = emp.FirstName,
        //        //                     LastName = emp.LastName,
        //        //                     HiringDate = emp.HiringDate,
        //        //                     IsActive = emp.IsActive,
        //        //                     Department = dep
        //        //                 }).ToList();
        //        //var employees = _dbContext.Employees.Include(x => x.Department).ToList();

        //        // var employeesDetails = _dbContext.Employees.Include("Department").ToList();

        //        var employees = _dbContext.Employees
        //                                .Include(x => x.Department)
        //                                .OrderBy(x => x.FirstName).ToList();
        //        return View(employees);
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //}

        public ActionResult Create()
        {

            HttpResponseMessage responseMessage = _httpClient.GetAsync(_baseUri + "/Department/GetAll").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = responseMessage.Content.ReadAsStringAsync().Result;

                var depList = JsonConvert.DeserializeObject<List<DepartmentModel>>(response);
                ViewBag.DepartmentList = depList;

            }


            return View();
        }




        [HttpPost]
        public ActionResult Create(EmployeeModel employee)
        {
            //if (employee != null)
            //{
            //    _dbContext.Employees.Add(employee);
            //    _dbContext.SaveChanges();
            //    return RedirectToAction(nameof(Index));
            //}

            using (var httpClient = new HttpClient())
            {

                httpClient.BaseAddress = new Uri(_baseUri + "/Employee");
                //StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");



                var responseMessage = httpClient.PostAsJsonAsync<EmployeeModel>("Employee", employee);
                responseMessage.Wait();
                var response = responseMessage.Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                }
            }

            //Reservation receivedReservation = new Reservation();
            //using (var httpClient = new HttpClient())
            //{
            //    StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

            //    using (var response = await httpClient.PostAsync("https://localhost:44324/api/Reservation", content))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
            //    }
            //}
            return View();
        }

        public ActionResult Edit(int id)
        {
            HttpResponseMessage responseMessage = _httpClient.GetAsync(_baseUri + "/Department/GetAll").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = responseMessage.Content.ReadAsStringAsync().Result;

                var depList = JsonConvert.DeserializeObject<List<DepartmentModel>>(response);
                ViewBag.DepartmentList = depList;

            }


            HttpResponseMessage empResponseMessage = _httpClient.GetAsync(_baseUri + $"/Employee/GetById/{id}").Result;
            if (empResponseMessage.IsSuccessStatusCode)
            {
                string response = empResponseMessage.Content.ReadAsStringAsync().Result;

                var employeeModel = JsonConvert.DeserializeObject<EmployeeModel>(response);

                return View("Create", employeeModel);

            }
            return View();

        }
        [HttpPost]
        public ActionResult Edit(int id, EmployeeModel employee)
        {

            using (var httpClient = new HttpClient())
            {

                httpClient.BaseAddress = new Uri(_baseUri + "/Employee");

                var responseMessage = httpClient.PutAsJsonAsync<EmployeeModel>("Employee", employee);
                responseMessage.Wait();
                var response = responseMessage.Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                }
            }


            //var employeeModel = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            //if (employeeModel != null)
            //{
            //    employeeModel.FirstName = employee.FirstName;
            //    employeeModel.LastName = employee.LastName;
            //    employeeModel.HiringDate = employee.HiringDate;
            //    employeeModel.IsActive = employee.IsActive;
            //    employeeModel.DOB = employee.DOB;
            //    employeeModel.BasicSalary = employee.BasicSalary;
            //    employeeModel.Email = employee.Email;
            //    employeeModel.DepartmentId = employee.DepartmentId;
            //    _dbContext.SaveChanges();
            //}
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            var employeeModel = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employeeModel != null)
            {
                _dbContext.Employees.Remove(employeeModel);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }


    }
}
