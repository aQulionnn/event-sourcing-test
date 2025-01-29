namespace projection.Events;

public record BookReceived(string Title, int Quantity, DateTime DateTime) : IEvent;

public record BookShipped(string Title, int Quantity, DateTime DateTime) : IEvent;
