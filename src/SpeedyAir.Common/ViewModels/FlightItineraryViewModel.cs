namespace SpeedyAir.Common.ViewModels
{
    public class FlightItineraryViewModel
    {
        public int? FlightId { get; set; }

        public string OrderId { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public int? Day { get; set; }

        public override string ToString()
        {
            if (FlightId == null)
            {
                return $"Order number: {OrderId}, Flight number: not scheduled";
            }

            return $"Order number: {OrderId}, Flight number: {FlightId}, Depurture: {Departure}, Arrival: {Arrival}, Day: {Day}";
        }
    }
}
