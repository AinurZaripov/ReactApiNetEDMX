using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCoreApiApp.Filters
{
    public class BaseResourceFilters : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Response.Headers.Add("content-range", "10");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method == "GET")
            {
                var contextResultObject = (context.Result as ObjectResult).Value;
                var property = typeof(ICollection).GetProperty("Count");
                int count = (int)property.GetValue(contextResultObject, null);
                context.HttpContext.Response.Headers.Add("content-range", count.ToString());
            }
        }
    }
}
