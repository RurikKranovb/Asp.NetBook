using Filters.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Controllers
{

    //[HttpsOnly]

    [ViewResultDetails]
    public class HomeController : Controller
    {

        //[RequireHttps]
        public ViewResult Index() => View("Message", "This is the Index action on the Home controller");


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
