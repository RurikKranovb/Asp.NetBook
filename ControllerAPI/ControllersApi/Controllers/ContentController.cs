using ControllersApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersApi.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        [HttpGet("string")]
        public string GetString() => "This is a string response";

        [HttpGet("object")]
        [FormatFilter]
       // [Produces("application/json")]
        public Reservation GetObject() => new Reservation()
        {
            ReservationId = 100,
            ClientName = "Joe",
            Location = "Board Room"
        };
    }
}
