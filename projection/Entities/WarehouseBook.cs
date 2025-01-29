using projection.Events;

namespace projection.Entities;

public class WarehouseBook
{
    public string Title { get; }
    private readonly IList<IEvent> _events = new List<IEvent>();
    private readonly CurrentState _currentState = new();

    public WarehouseBook(string title)
    {
        Title = title;
    }

    public void ShipBook(int quantity)
    {
        AddEvent(new BookShipped(Title, quantity, DateTime.Now));
    }

    public void RecieveBook(int quantity)
    {
        AddEvent(new BookReceived(Title, quantity, DateTime.Now));
    }

    private void Apply(BookReceived evnt)
    {
        _currentState.QuantityOnHand += evnt.Quantity;
    }

    private void Apply(BookShipped evnt)
    {
        _currentState.QuantityOnHand -= evnt.Quantity;
    }

    public IList<IEvent> GetEvents()
    {
        return _events;    
    }
    
    public void AddEvent(IEvent evnt)
    {
        switch (evnt)
        {
            case BookReceived bookReceived:
                Apply(bookReceived);
                break;
            case BookShipped bookShipped:
                Apply(bookShipped);
                break;
        }
        
        _events.Add(evnt);
    }

    public int GetQuantityOnHand()
    {
        return _currentState.QuantityOnHand;
    }
}