using projection.Entities;
using projection.Events;

namespace projection.Repositories;

public class WarehouseBookRepository
{
    private readonly Dictionary<string, IList<IEvent>> _inMemory = new();
    

    public WarehouseBook Get(string title)
    {
        var warehouseBook = new WarehouseBook(title);

        if (_inMemory.ContainsKey(title))
        {
            foreach (var evnt in _inMemory[title])
            {
                warehouseBook.AddEvent(evnt);
            }
        }
        
        return warehouseBook;
    }

    public void Save(WarehouseBook warehouseBook)
    {
        _inMemory[warehouseBook.Title] = warehouseBook.GetEvents();
    }
}