using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersDemo.Filters
{
    public class MyActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("ActionFilter OnActionExecuting Method");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("ActionFilter OnActionExecuted Method");
        }

    }
}
