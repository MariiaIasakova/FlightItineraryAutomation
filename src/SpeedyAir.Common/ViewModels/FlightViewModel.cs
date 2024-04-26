namespace SpeedyAir.Common.ViewModels
{
    public class FlightViewModel
    {
        public int FlightId { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public int Day { get; set; }

        public override string ToString()
        {
            return $"Flight number: {FlightId}, Depurture: {Departure}, Arrival: {Arrival}, Day: {Day}";
        }
    }
}
