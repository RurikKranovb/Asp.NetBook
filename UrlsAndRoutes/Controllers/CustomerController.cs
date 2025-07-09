using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    [Route("app/[controller]/action/[action]/{id:weekday?}")]
    public class CustomerController : Controller
    {
        [Route("[controller]/MyAction")]
        public ViewResult Index() => View("Result",
            new Result()
            {
                Controller = nameof(CustomerController),
                Action = nameof(Index)
            });

        public ViewResult List() => View("Result",
            new Result()
            {
                Controller = nameof(CustomerController),
                Action = nameof(List)
            });

        public ViewResult List(string? id)
        {
            Result r = new Result()
            {
                Controller = nameof(HomeController),
                Action = nameof(HomeController),
            };

            r.Data["id"] = id ?? "<no value>";
            r.Data["catchall"] = RouteData.Values["catchall"];
            return View("Result", r);
        }
    }
}
