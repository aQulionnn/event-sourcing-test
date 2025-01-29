using Microsoft.AspNetCore.Mvc;
using projection.Events;
using projection.Repositories;

namespace projection.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly WarehouseBookRepository _repo;

    public BookController(WarehouseBookRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var warehouseBook = _repo.Get("Dune");
        
        warehouseBook.RecieveBook(10);
        warehouseBook.ShipBook(5);
        
        warehouseBook.RecieveBook(1);

        var list = new List<string>();
        foreach (var evnt in warehouseBook.GetEvents())
        {
            switch (evnt)
            {
                case BookShipped bookShipped:
                    list.Add($"Shipped: {bookShipped.Quantity}");
                    break;
                case BookReceived bookReceived:
                    list.Add($"Received: {bookReceived.Quantity}");
                    break;
            }
        }
        
        return Ok(list);
    }
}