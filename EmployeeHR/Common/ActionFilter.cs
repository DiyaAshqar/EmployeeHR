using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.Web;

namespace EmployeeHR.Common
{
    public class ActionFilter : ActionFilterAttribute, IExceptionFilter
    {


        public override void OnActionExecuting(ActionExecutingContext context)
        {

            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.RouteData.Values["controller"].ToString();


            string message = $"\n{controllerName} Controller ,{actionName} Action " +
                $"- ActionFilter-OnActionExecuting, {DateTime.Now.ToString()} \n";
            //LogException(message);

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        public void OnException(ExceptionContext context)
        {
            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.RouteData.Values["controller"].ToString();


            string message = $"\n{controllerName} Controller ,{actionName} Action " +
                $"- OnActionExecuting - Exception: {context.Exception.Message}, {DateTime.Now.ToString()} \n";
            //LogException(message);
        }
    }
}
