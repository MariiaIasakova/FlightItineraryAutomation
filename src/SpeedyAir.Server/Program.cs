using SpeedyAir.Data.Repositories;
using SpeedyAir.Data.Repositories.Interfaces;
using SpeedyAir.Server.Configuration;
using SpeedyAir.Server.HostedServices;
using SpeedyAir.Server.Services;
using SpeedyAir.Server.Services.Interfaces;
using System.Text.Json;

namespace SpeedyAir.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add options
            builder.Services.Configure<FlightOptions>(builder.Configuration.GetSection(nameof(FlightOptions)));
            builder.Services.AddSingleton(new JsonSerializerOptions(JsonSerializerDefaults.Web));


            // Add services to the container.
            // Repositories
            builder.Services.AddScoped<IScheduleRepository, InMemoryScheduleRepository>();
            builder.Services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
            builder.Services.AddScoped<IFlightItinerariesRepository, InMemoryFlightItinerariesRepository>();
            builder.Services.AddScoped<IFlightItineraryService, FlightItineraryService>();

            // Services
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Hosted services
            builder.Services.AddHostedService<ScheduleProviderService>();
            builder.Services.AddHostedService<OrderProviderService>();

            var app = builder.Build();

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
