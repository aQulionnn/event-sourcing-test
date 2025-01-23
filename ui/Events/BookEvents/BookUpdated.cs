namespace ui.Events.BookEvents;

public class BookUpdated : Event
{
    public required Guid BookId { get; init; }
    public required double Price { get; init; }
    
    public override Guid StreamId => BookId;
}