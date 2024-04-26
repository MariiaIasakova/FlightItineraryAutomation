using SpeedyAir.Data.DTO;
using SpeedyAir.Data.Repositories.Interfaces;
using SpeedyAir.Server.Services.Interfaces;
using System.Text.Json;

namespace SpeedyAir.Server.HostedServices
{
    public class OrderProviderService : BackgroundService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IServiceProvider _serviceProvider;
        private readonly JsonSerializerOptions _serializerOptions;

        public OrderProviderService(
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
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var flightItineraryService = scope.ServiceProvider.GetRequiredService<IFlightItineraryService>();
            var ordersJson = File.ReadAllText(Path.Combine(_hostingEnvironment.WebRootPath, "coding-assigment-orders.json"));
            var orderDetails = JsonSerializer.Deserialize<IDictionary<string, OrderDetails>>(ordersJson, _serializerOptions);
            if (orderDetails != null)
            {
                var orders = orderDetails.Select(x => new Order
                {
                    OrderId = x.Key,
                    Destination = x.Value.Destination,
                }).ToList();
                await orderRepository.UploadOrdersAsync(orders, stoppingToken);
                await flightItineraryService.ProcessFlightItinerariesAsync(stoppingToken);
            }

            // Log warning
        }

        private class OrderDetails
        {
            public string Destination { get; set; }
        }
    }
}
