using Microsoft.AspNetCore.Mvc;

namespace Filters.Controllers
{
    public class ClobalController : Controller
    {
        public ViewResult Index() => View("Message", $"This is the global controller");
    }
}
