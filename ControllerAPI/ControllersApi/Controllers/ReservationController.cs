using ControllersApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersApi.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository _repository;

        public ReservationController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Reservation> Get() => _repository.Reservations;

        [HttpGet("{id}")]
        public Reservation Get(int id) => _repository[id];

        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
            _repository.AddReservation(new Reservation()
            {
                ClientName = res.ClientName,
                Location = res.Location,
            });

        [HttpPut]
        public Reservation Put([FromBody] Reservation res) =>
            _repository.UpdateReservation(res);

        [HttpDelete("{id}")]
        public void Delete(int id) => _repository.DeleteReservation(id);

    }
}
