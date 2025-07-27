using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    public class ViewResultDetailsAttribute : ResultFilterAttribute
    {

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                ["Result Type"] = context.Result.GetType().Name

            };

            ViewResult vr;

            if ((vr = context.Result as ViewResult) != null)
            {
                dict["View Name"] = vr.ViewName;
                dict["Model Type"] = vr.ViewData.Model.GetType().Name;
                dict["Model Data"] = vr.ViewData.Model.ToString();

            }

            await next();
        }

        //public override void OnResultExecuting(ResultExecutingContext context)
        //{
        //    Dictionary<string, string> dict = new Dictionary<string, string>
        //    {
        //        ["Result Type"] = context.Result.GetType().Name

        //    };

        //    ViewResult vr;

        //    if ((vr = context.Result as ViewResult) != null)
        //    {
        //        dict["View Name"] = vr.ViewName;
        //        dict["Model Type"] = vr.ViewData.Model.GetType().Name;
        //        dict["Model Data"] = vr.ViewData.Model.ToString();

        //    }
        //}
    }
}
