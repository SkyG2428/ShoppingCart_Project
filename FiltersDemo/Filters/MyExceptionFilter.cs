using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersDemo.Filters
{
    public class MyExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //Console.WriteLine("Exception Occured");

            string exceptionMessage = context.Exception.Message;
            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();

            context.ExceptionHandled = true;

            context.Result = new RedirectToActionResult("ErrorView", "Home", null);
            context.Result.Equals(exceptionMessage);
        }
    }
}
