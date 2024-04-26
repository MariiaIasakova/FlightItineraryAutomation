using SpeedyAir.Data.DTO;
using SpeedyAir.Data.Repositories.Interfaces;

namespace SpeedyAir.Data.Repositories
{
    public class InMemoryFlightItinerariesRepository : IFlightItinerariesRepository
    {
        private static ICollection<FlightItinerary> _flightItineraries;
        public Task<ICollection<FlightItinerary>> GetAllFlightOrdersAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_flightItineraries);
        }

        public Task<ICollection<FlightItinerary>> GetFlightOrdersAsync(int flightId, CancellationToken cancellationToken)
        {
            ICollection<FlightItinerary> result = _flightItineraries.Where(x => x.Flight?.FlightId == flightId).ToList();
            return Task.FromResult(result);
        }

        public Task UploadAllFlightItinerariesAsync(ICollection<FlightItinerary> flightItineraries, CancellationToken cancellationToken)
        {
            _flightItineraries = flightItineraries;
            return Task.CompletedTask;
        }
    }
}
