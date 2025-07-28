using Filters.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Controllers
{

    //[HttpsOnly]

    //[ViewResultDetails]
    //[Profile]
    //[RangeException]
    [TypeFilter(typeof(DiagnosticsFilter))]
    [TypeFilter(typeof(TimerFilter))]
    public class HomeController : Controller
    {

        //[RequireHttps]
        public ViewResult Index() => View("Message", "This is the Index action on the Home controller");

        public ViewResult GenerateException(int? id)
        {
            if (id==null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            else if (id > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            else
            {
                return View("Message", $"The valie is {id}");
            }
        }


        //public IActionResult Index()
        //{
        //    if (!Request.IsHttps)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status403Forbidden);
        //    }
        //    else
        //    {
        //        return View("Message", "This is the Index action on the Home controller");
        //    }
        //}
        //public ViewResult Index() => View("Message", "This is the Index action on the Home controller");
    }
}
