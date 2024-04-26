using Microsoft.AspNetCore.Mvc;
using SpeedyAir.Common.ViewModels;
using SpeedyAir.Data.DTO;
using SpeedyAir.Data.Repositories.Interfaces;

namespace SpeedyAir.Server.Controllers
{
    [Route("api/[controller]")]
    public class FlightItineraryController : Controller
    {
        private readonly IFlightItinerariesRepository _flightItinerariesRepository;

        public FlightItineraryController(IFlightItinerariesRepository flightItinerariesRepository)
        {
            _flightItinerariesRepository = flightItinerariesRepository;
        }

        [HttpGet]
        public async Task<ICollection<FlightItineraryViewModel>> GetAsync(CancellationToken cancellationToken)
        {
            var result = await _flightItinerariesRepository.GetAllFlightOrdersAsync(cancellationToken);
            return MapToViewModel(result);
        }

        [Route("{flightId}")]
        [HttpGet]
        public async Task<ICollection<FlightItineraryViewModel>> GetByFlightAsync([FromRoute] int flightId, CancellationToken cancellationToken)
        {
            var result = await _flightItinerariesRepository.GetFlightOrdersAsync(flightId,cancellationToken);
            return MapToViewModel(result);
        }

        private ICollection<FlightItineraryViewModel> MapToViewModel(ICollection<FlightItinerary> flightItineraries)
        {
            return flightItineraries.Select(x => new FlightItineraryViewModel
            {
                FlightId = x.Flight?.FlightId,
                OrderId = x.Order.OrderId,
                Departure = x.Flight?.DepartureCity,
                Arrival = x.Flight?.ArrivalCity,
                Day = x.Flight?.Day
            }).ToList();
        }
    }
}
