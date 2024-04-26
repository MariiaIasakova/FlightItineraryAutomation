using System.Text.Json.Serialization;

namespace SpeedyAir.Data.DTO
{
    public class Flight
    {

        [JsonPropertyName("flight_number")]
        public int FlightId { get; set; }

        [JsonPropertyName("departure_city")]
        public string DepartureCity { get; set; }

        [JsonPropertyName("arrival_city")]
        public string ArrivalCity { get; set; }

        public int Day { get; set; }
    }
}