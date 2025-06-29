using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        private UpTimeService _upTime;
        private readonly ILogger<HomeController> _log;

        public HomeController(UpTimeService up, ILogger<HomeController> log)
        {
            _upTime = up;
            _log = log;
        }


        public ViewResult Index(bool throwException = false)
        {
            _log.LogDebug($"Handled {Request.Path} at uptime {_upTime.UpTime}");

            if (throwException)
            {
                throw new System.NullReferenceException();
            }

            return View(new Dictionary<string, string>()
            {
                ["Message"] = "This is the Index action",
                ["UpTime"] = $"{_upTime.UpTime}ms"
            });
        }

        public ViewResult Error() => View(nameof(Index),
            new Dictionary<string, string>()
            {
                ["Message"] = "This is the Error action"
            });

    }
}
