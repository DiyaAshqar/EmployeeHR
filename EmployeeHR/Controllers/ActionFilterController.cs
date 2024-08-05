using Microsoft.AspNetCore.Mvc;
using EmployeeHR.Common;

namespace EmployeeHR.Controllers
{
    public class ActionFilterController : Controller
    {
        [ActionFilter]
        public string Index()
        {
            return "List of object";
        }

       
        [ActionFilter]
        public string Create()
        {
            throw new Exception("Exception happened");
        }
    }
}
