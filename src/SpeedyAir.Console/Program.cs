using SpeedyAir.Console.Api;

using HttpClient client = new()
{
    BaseAddress = new Uri("http://localhost:5177"),
};

var scheduleHttpClient = new ScheduleHttpClient(client);
var schedule = await scheduleHttpClient.GetScheduleAsync();

Console.WriteLine("Schedule:");
Console.WriteLine(string.Join(Environment.NewLine, schedule));

var flightItineraryHttpClient = new FlightItineraryHttpClient(client);
var flightItineraries = await flightItineraryHttpClient.GetFlightItineraryAsync();

Console.WriteLine("Orders:");
Console.WriteLine(string.Join(Environment.NewLine, flightItineraries));
Console.ReadKey();