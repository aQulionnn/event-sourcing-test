using projection.Entities;
using projection.Events;

namespace projection.Projections;

public class BookProjection
{
    private readonly AppDbContext _context;

    public BookProjection(AppDbContext context)
    {
        _context = context;
    }
    
    public void RecieveEvent(IEvent evnt) 
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
    }

    public Book GetBook(string title)
    {
        var book = _context.Books.SingleOrDefault(x => x.Title == title);
        if (book == null)
        {
            book = new Book()
            {
                Title = title
            };
            _context.Books.Add(book);
        }
        
        return book;
    }

    private void Apply(BookReceived bookReceived)
    {
        var state = GetBook(bookReceived.Title);
        state.Recieved += bookReceived.Quantity;
        _context.SaveChanges();
    }

    private void Apply(BookShipped bookShipped)
    {
        var book = GetBook(bookShipped.Title);
        book.Shipped += bookShipped.Quantity;
        _context.SaveChanges();
    }
}