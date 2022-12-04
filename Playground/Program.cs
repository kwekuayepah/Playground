// See https://aka.ms/new-console-template for more information

namespace Playground;

public class Program
{
    
    public static readonly IDictionary<string, City> Cities = new Dictionary<string, City>()
    {
        { "New York", new City("New York", 3572, 1455) },
        { "Los Angeles", new City("Los Angeles", 462, 975) },
        { "San Francisco", new City("San Francisco", 183, 1233) },
        { "Boston", new City("Boston", 3778, 1566) },
        { "Chicago", new City("Chicago", 2608, 1525) },
        { "Washington", new City("Washington", 3358, 1320) },
    };
    
    static void Main(string[] args)
    {
      
            
        var events = new List<Event>{
            new Event(1, "Phantom of the Opera", "New York", new DateTime(2023,12,23)),
            new Event(2, "Metallica", "Los Angeles", new DateTime(2023,12,02)),
            new Event(3, "Metallica", "New York", new DateTime(2023,12,06)),
            new Event(4, "Metallica", "Boston", new DateTime(2023,10,23)),
            new Event(5, "LadyGaGa", "New York", new DateTime(2023,09,20)),
            new Event(6, "LadyGaGa", "Boston", new DateTime(2023,08,01)),
            new Event(7, "LadyGaGa", "Chicago", new DateTime(2023,07,04)),
            new Event(8, "LadyGaGa", "San Francisco", new DateTime(2023,07,07)),
            new Event(9, "LadyGaGa", "Washington", new DateTime(2023,05,22)),
            new Event(10, "Metallica", "Chicago", new DateTime(2023,01,01)),
            new Event(11, "Phantom of the Opera", "San Francisco", new DateTime(2023,07,04)),
            new Event(12, "Phantom of the Opera", "Chicago", new DateTime(2024,05,15))
        };
            
        var customer = new Customer()
        {
            Id = 1,
            Name = "John",
            City = "New York",
            BirthDate = new DateTime(1995, 05, 10)
        };

        var orderByName = events.GetSortedEventsByField("Name");

        var marketingEngine = new MarketingEngine(events);
        marketingEngine.AlertCustomerOnEventCloseToBirthday(customer, events);
        marketingEngine.Get5EventsClosestToCustomer(customer, Cities);
        marketingEngine.CacheClosestCities("New York - Boston");


    }
}