using SpeedyAir.Data.DTO;

namespace SpeedyAir.Server.Services.Interfaces
{
    public interface IFlightItineraryService
    {
        /// <summary>
        /// Returns flight itineraries.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ProcessFlightItinerariesAsync(CancellationToken cancellationToken);
    }
}
