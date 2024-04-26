using SpeedyAir.Data.DTO;

namespace SpeedyAir.Data.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        /// <summary>
        /// Returns all flights.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of flights.</returns>
        Task<ICollection<Flight>> GetFlightsAsync(CancellationToken cancellationToken);


        /// <summary>
        /// Uploads flights schedule.
        /// </summary>
        /// <param name="flights">Flights.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UploadScheduleAsync(ICollection<Flight> flights, CancellationToken cancellationToken);
    }
}
