using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersDemo.Filters
{
    public class MyResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("ResourceFilter OnResourceExecuting Method");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("ResourceFilter OnResourceExecuted Method");
        }
     
    }

    public class MyResourceFilter2 : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("ResourceFilter OnResourceExecuting2 Method");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("ResourceFilter OnResourceExecuted2 Method");
        }

    }

}
