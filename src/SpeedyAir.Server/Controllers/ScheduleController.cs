using Microsoft.AspNetCore.Mvc;
using SpeedyAir.Data.Repositories.Interfaces;
using SpeedyAir.Common.ViewModels;

namespace SpeedyAir.Server.Controllers
{
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleController(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        [HttpGet]
        public async Task<ICollection<FlightViewModel>> GetAsync(CancellationToken cancellationToken)
        {
            var flights = await _scheduleRepository.GetFlightsAsync(cancellationToken);
            return flights.Select(x => new FlightViewModel
            {
                FlightId = x.FlightId,
                Departure = x.DepartureCity,
                Arrival = x.ArrivalCity,
                Day = x.Day,
            }).ToList();
        }
    }
}
