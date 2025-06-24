using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        private UpTimeService _upTime;

        public HomeController(UpTimeService up) => _upTime = up;

        public ViewResult Index()
            => View(new Dictionary<string, string>()
            {
                ["Message"] = "This is the Index action",
                ["UpTime"] = $"{_upTime.UpTime}ms"
            });
    }
}
