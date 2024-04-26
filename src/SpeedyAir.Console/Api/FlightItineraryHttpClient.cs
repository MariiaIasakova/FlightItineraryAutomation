using SpeedyAir.Common.ViewModels;
using System.Net.Http.Json;

namespace SpeedyAir.Console.Api
{
    public class FlightItineraryHttpClient
    {
        private readonly HttpClient _client;
        public FlightItineraryHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyCollection<FlightItineraryViewModel>> GetFlightItineraryAsync()
        {
            var response = await _client.GetAsync("api/flightItinerary");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to load data from API. Response status = {response.StatusCode}");
            }

            var flightItineraries = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<FlightItineraryViewModel>>();
            return flightItineraries;

        }
    }
}
