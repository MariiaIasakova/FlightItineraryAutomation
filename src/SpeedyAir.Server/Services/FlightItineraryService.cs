using Microsoft.Extensions.Options;
using SpeedyAir.Data.DTO;
using SpeedyAir.Data.Repositories.Interfaces;
using SpeedyAir.Server.Configuration;
using SpeedyAir.Server.Services.Interfaces;

namespace SpeedyAir.Server.Services
{
    public class FlightItineraryService : IFlightItineraryService
    {

        private readonly IScheduleRepository _scheduleRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IFlightItinerariesRepository _flightItinerariesRepository;
        private readonly FlightOptions _flightOptions;

        public FlightItineraryService(
            IScheduleRepository scheduleRepository,
            IOrderRepository orderRepository,
            IFlightItinerariesRepository flightItinerariesRepository,
            IOptions<FlightOptions> flightOptions)
        {
            _scheduleRepository = scheduleRepository;
            _orderRepository = orderRepository;
            _flightItinerariesRepository = flightItinerariesRepository;
            _flightOptions = flightOptions?.Value ?? new FlightOptions { FlightCapacity = 20 };
        }

        public async Task ProcessFlightItinerariesAsync(CancellationToken cancellationToken)
        {
            var data = await GetFlightItinerariesAsync(cancellationToken);
            await _flightItinerariesRepository.UploadAllFlightItinerariesAsync(data, cancellationToken);
        }

        private async Task<ICollection<FlightItinerary>> GetFlightItinerariesAsync(CancellationToken cancellationToken)
        {
            var scheduleTask = _scheduleRepository.GetFlightsAsync(cancellationToken);
            var ordersTask = _orderRepository.GetOrdersAsync(cancellationToken);
            await Task.WhenAll(scheduleTask, ordersTask);

            var schedule = scheduleTask.Result;
            var orders = ordersTask.Result;
            var capacity = _flightOptions.FlightCapacity;

            var flightItireres = new List<FlightItinerary>();

            if(schedule == null || orders == null)
            {
                return flightItireres;
            }

            var ordersByDestination = orders.GroupBy(x => x.Destination).ToDictionary(x => x.Key, x => x.ToList(), StringComparer.OrdinalIgnoreCase);
            foreach (var flight in schedule)
            {
                if (!ordersByDestination.ContainsKey(flight.ArrivalCity))
                {
                    // this flight is not used because there are no orders for this destination
                    continue;
                }

                var ordersToDestination = ordersByDestination[flight.ArrivalCity];
                var ordersToCurrentFlight = ordersToDestination.Take(capacity).ToList();
                if (!ordersToCurrentFlight.Any())
                {
                    continue;
                }

                flightItireres.AddRange(ordersToCurrentFlight.Select(x => new FlightItinerary
                {
                    Flight = flight,
                    Order = x
                }));

                ordersToDestination.RemoveRange(0, ordersToCurrentFlight.Count);
            }

            flightItireres.AddRange(ordersByDestination.SelectMany(x => x.Value).Select(x => new FlightItinerary
            {
                Order = x
            }));

            return flightItireres;
        }
    }
}
