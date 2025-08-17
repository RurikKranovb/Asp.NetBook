using ControllersApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersApi.Controllers
{
    public class HomeController : Controller
    {
        private IRepository Repository { get; set; }

        public HomeController(IRepository repository)
        {
            Repository = repository;
        }

        public ViewResult Index() => View(Repository.Reservations);

        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            Repository.AddReservation(reservation);
            return RedirectToAction("Index");
        }

    }
}
