using SpeedyAir.Common.ViewModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace SpeedyAir.Console.Api
{
    public class ScheduleHttpClient
    {
        private readonly HttpClient _client;
        public ScheduleHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyCollection<FlightViewModel>> GetScheduleAsync()
        {
            var response = await _client.GetAsync("api/schedule");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to load data from API. Response status = {response.StatusCode}");
            }

            var schedule = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<FlightViewModel>>();
            return schedule;
        }
    }
}
