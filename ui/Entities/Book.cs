using ui.Events;
using ui.Events.BookEvents;

namespace ui.Entities;

public class Book 
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public double Price { get; private set; }
    
    public ICollection<Event> Events { get; } = new List<Event>();

    private Book() { }

    public static Book Create(string title, double price)
    {
        var book = new Book();
        var @event = new BookCreated()
        {
            BookId = Guid.NewGuid(),
            Title = title,
            Price = price
        };
        
        book.Apply(@event);
        return book;
    }

    public void Updated(double price)
    {
        Apply(new BookUpdated()
        {
            BookId = Id,
            Price = price
        });
    }
    
    public void Apply(Event @event)
    {
        switch (@event)
        {
            case BookCreated e:
                Id = e.BookId;
                Title = e.Title;
                Price = e.Price;
                break;
            
            case BookUpdated e:
                Id = e.BookId;
                Price = e.Price;
                break;
        }
        
        Events.Add(@event);
    }

    public static Book ReplayEvents(IEnumerable<Event> events)
    {
        var book = new Book();
        foreach (var @event in events)
        {
            book.Apply(@event);
        }
        
        return book;
    }
}