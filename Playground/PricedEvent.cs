namespace Playground;

public class PricedEvent : Event
{
    public decimal Price { get; set; }

    public PricedEvent(int id, string name, string city, DateTime date, decimal price) : base(id, name, city, date)
    {
        Price = price;
    }
}