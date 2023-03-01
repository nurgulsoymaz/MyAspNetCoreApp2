using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MyAspNetCoreApp2.Web.Filters
{
    public class LogFilter :ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("Action Method çalışmadan önce"); //Action filter
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Action Method çalıştıktan sonra"); //Action filter
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("Action Method sonuç üretilmeden  önce"); //result filter
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Debug.WriteLine("Action Method sonuç üretildikten sonra"); //result filter
        }

    }
}
