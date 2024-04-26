using SpeedyAir.Data.DTO;
using SpeedyAir.Data.Repositories.Interfaces;
namespace SpeedyAir.Data.Repositories
{
    public class InMemoryScheduleRepository : IScheduleRepository
    {
        private static ICollection<Flight> _flights;

        public Task<ICollection<Flight>> GetFlightsAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_flights);
        }

        public Task UploadScheduleAsync(ICollection<Flight> flights, CancellationToken cancellationToken)
        {
            _flights = flights;
            return Task.CompletedTask;
        }
    }
}
