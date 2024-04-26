using SpeedyAir.Data.DTO;
using SpeedyAir.Data.Repositories.Interfaces;
using SpeedyAir.Server.Services.Interfaces;
using System.Text.Json;

namespace SpeedyAir.Server.HostedServices
{
    public class ScheduleProviderService : BackgroundService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IServiceProvider _serviceProvider;
        private readonly JsonSerializerOptions _serializerOptions;

        public ScheduleProviderService(
            IWebHostEnvironment hostingEnvironment,
            IServiceProvider serviceProvider,
            JsonSerializerOptions serializerOptions)
        {
            _hostingEnvironment = hostingEnvironment;
            _serviceProvider = serviceProvider;
            _serializerOptions = serializerOptions;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var scheduleRepository = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();
            var flightItineraryService = scope.ServiceProvider.GetRequiredService<IFlightItineraryService>();
            var scheduleJson = File.ReadAllText(Path.Combine(_hostingEnvironment.WebRootPath, "coding-assignment-schedule.json"));
            var flights = JsonSerializer.Deserialize<ICollection<Flight>>(scheduleJson, _serializerOptions);
            if (flights != null)
            {
                await scheduleRepository.UploadScheduleAsync(flights, stoppingToken);
                await flightItineraryService.ProcessFlightItinerariesAsync(stoppingToken);
            }
            // Log warning
        }
    }
}
