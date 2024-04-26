using SpeedyAir.Data.DTO;

namespace SpeedyAir.Data.Repositories.Interfaces
{
    public interface IFlightItinerariesRepository
    {
        /// <summary>
        /// Returns all flight orders.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of all flight orders.</returns>
        Task<ICollection<FlightItinerary>> GetAllFlightOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Returns all flight orders.
        /// </summary>
        /// <param name="flightId">Flight identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of flight orders.</returns>
        Task<ICollection<FlightItinerary>> GetFlightOrdersAsync(int flightId, CancellationToken cancellationToken);

        /// <summary>
        /// Uploads flights itineraries.
        /// </summary>
        /// <param name="flightItinerary">Flight itinerarie.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UploadAllFlightItinerariesAsync(ICollection<FlightItinerary> flightItineraries, CancellationToken cancellationToken);

    }
}
