namespace Playground;

public record City(string Name, int X, int Y);

public class MarketingEngine
{
    private readonly List<Event> Events;

    private IDictionary<string, int> cityDistance = new Dictionary<string, int>();

      public MarketingEngine(List<Event> events)
    {
        Events = events;
    }

    public void CacheClosestCity(string location)
    {

        foreach(var value in cityDistance)
        {
            Console.WriteLine($"{value.Key} and {value.Value}");
        }
        
        var distance =0;

        var locations = location.Split(" - ").OrderByDescending(x=>x).ToList();

        var key = $"{locations[0]} - {locations[1]}";

        var locationExist = cityDistance.ContainsKey(key);

        if(!locationExist)
        {
            distance = 0;
        }
        else
        {
            distance = cityDistance[key];
        }

        Console.WriteLine($"The distance between {location} is {distance}");
    }

    public void AlertCustomerEventsClosestToCustomer(Customer customer,IDictionary<string, City> cities, int eventSize)
    {
        try
        {
            var customerCityExist = cities.ContainsKey(customer.City);

            if(!customerCityExist)
            {
                return;
            }

            var customerCityInfo = cities[customer.City];

            var cityToDistanceDict = cities.Values.ToDictionary(x=>x.Name,
                x=> Math.Abs(customerCityInfo.X - x.X) + Math.Abs(customerCityInfo.Y - x.Y));

            foreach(var value in cityToDistanceDict)
            {
                cityDistance.Add($"{customer.City} - {value.Key}", value.Value);
            }

            var events = Events.OrderBy(x=>cityToDistanceDict[x.City]).Take(eventSize).ToList();

            foreach(var item in events)
            { 
                SendCustomerNotification(customer, item);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred when retrieving city distance" +
                              $"message: {e.Message}" +
                              $"stack trace: {e}");
        }
      

    }

    private void SendCustomerNotification(Customer customer, Event e)
    {
        Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date} at City {e.City}");
    }

    public void AlertCustomerOnEventsNearestToCity(Customer customer, int eventSize)
    {
        var customerBirthDate = new DateTime(DateTime.UtcNow.Year, customer.BirthDate.Month, customer.BirthDate.Day);

        var currentDate = DateTime.UtcNow.Date;

        var eventYear = (currentDate > customerBirthDate)? customerBirthDate.Year +1 : customerBirthDate.Year;

        var events = Events.Where(x=>x.Date.Month >= customerBirthDate.Month && x.Date.Year == eventYear).OrderBy(x=>x.Date.Month).ToList();

        foreach(var item in events.Take(eventSize))
        {
            SendCustomerNotification(customer, item);
        }
    }

    public void AlertCustomerOnEventsInCity(Customer customer)
    {
        var events = Events.Where(x=>string.Equals(x.City, customer.City)).ToList();

        foreach(var item in events)
        {
            SendCustomerNotification(customer, item);
        }
    }
}
