namespace Playground;

public static class EventExtension
{
    public static List<Event> GetSortedEventsByField(this List<Event> events, string data)
    {
        
        var propertyInfo = typeof(Event).GetProperty(data);    
        var orderByFieldEvents = events.OrderBy(x => propertyInfo?.GetValue(x, null)).ToList();

        return orderByFieldEvents;
    }
}