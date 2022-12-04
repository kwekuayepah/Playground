namespace Playground;

public record City(string Name, int X, int Y);

public class MarketingEngine
{
    private readonly List<Event> Events;


    public MarketingEngine(List<Event> events)
    {
        Events = events;
    }

    public void SendNotifications(Customer customer, Event e)
    {
        Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date}");
    }


    public void AlertCustomerOnEventCloseToBirthday(Customer customer, List<Event> events)
    {
        /*
     * 1. Get the customer's birth day and month against the current year
     * 2. if the current date is greater than the customer date, at one to year, else year remains the same
     * 3. Use lambda expression to filter records with condition above
     * 4. Also select a customer events in city
     * 5. Add both and send notification to user
     */
         
        var customerBirthDate = new DateTime(DateTime.UtcNow.Year, customer.BirthDate.Month, customer.BirthDate.Day);
        var currentDate = DateTime.UtcNow.Date;

        var eventYear = (currentDate > customerBirthDate) ? customerBirthDate.Year + 1 : customerBirthDate.Year;


        var customerBirthMonthEvents = events.Where(
            x => x.Date.Month == customer.BirthDate.Month && x.Date.Year == eventYear
        ).ToList();


        var customerCityEvents = events.Where(
            x => string.Equals(x.City, customer.City) && x.Date >= currentDate
        ).ToList();

        customerBirthMonthEvents.AddRange(customerCityEvents);

        foreach (var item in customerBirthMonthEvents.Distinct())
        {
            SendNotifications(customer, item);
        }
    }

    public void Get5EventsClosestToCustomer(Customer customer, IDictionary<string,City> cities)
    {
        try
        {
            var customerCityExist = cities.ContainsKey(customer.City);

            if (!customerCityExist)
                return;
        
            var customerCity = cities[customer.City];

            IDictionary<string, int> cityToDistanceDict = cities.Values.ToDictionary(x =>
                    x.Name, x => Math.Abs(customerCity.X - x.X) + Math.Abs(customerCity.Y - x.Y)
            );
        
            var events = Events.OrderBy(e => cityToDistanceDict[e.City]).Take(5).ToList();

            foreach (var item in events)
            {
                SendNotifications(customer, item);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("An exception occured when getting 5 closest events to the customer" +
                              $"message: {e.Message}" +
                              $"stack trace: {e}");
            
        }
      

    }
    
    public void CacheClosestCities(string location)
    {
        /*
     * 1. We will used redis string set data structure
     * 2. the governance here is that the redis key should be in an ordered in ascending
     * 3. from the function, split with hyphen and order list to ascending
     * 4. Form the key structure to retrieve data from redis
     * 5. if key does not exist , fetch from API
     */
        var distance = 0;
        IDictionary<string, int> cities = new Dictionary<string, int>()
        {
            { "Boston - New York",400 },
            { "Boston - Washington", 540 }
        };

        var locationBucket = location.Split(" - ").OrderBy(x => x).ToList();

        if (!cities.ContainsKey($"{locationBucket[0]} - {locationBucket[1]}"))
        {
            //fetch from API
            //store in cache
            //assign value to distance
        }
        else
        {
            distance = cities[$"{locationBucket[0]} - {locationBucket[1]}"];
        }
        
        Console.WriteLine($"distance: {distance}");
    }
    

   
}