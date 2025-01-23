namespace ui.Events.BookEvents;

public class BookCreated : Event
{
    public required Guid BookId { get; init; }
    public required string Title { get; init; }
    public required double Price { get; init; }
    
    public override Guid StreamId => BookId;
}