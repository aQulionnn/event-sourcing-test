using Marten;
using Microsoft.AspNetCore.Mvc;

namespace with_marten;

[Route("api/orders")]
[ApiController]
public class OrderController(IDocumentStore store, IQuerySession session) : ControllerBase
{
    private readonly IDocumentStore _store = store;
    private readonly IQuerySession _session = session;
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _session.Query<Order>().ToListAsync();
        return Ok(orders);
    }
    
    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid orderId)
    {
        var order = await _session.LoadAsync<Order>(orderId);
        return order is not null ? Ok(order) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = new Events.OrderCreated
        {
            ProductName = request.ProductName,
            DeliveryAddress = request.DeliveryAddress
        };
        
        await using var session = _store.LightweightSession();
        session.Events.StartStream<Order>(order.Id, order);
        await session.SaveChangesAsync();
        
        return Ok(order);
    }

    [HttpPost("{orderId:guid}/address")]
    public async Task<IActionResult> DeliveryAddressUpdate([FromRoute] Guid orderId, [FromBody] DeliveryAddressUpdateRequest request)
    {
        var addressUpdated = new Events.OrderAddressUpdated
        {
            Id = orderId,
            DeliveryAddress = request.DeliveryAddress
        };
        
        await using var session = _store.LightweightSession();
        session.Events.Append(orderId, addressUpdated);
        await session.SaveChangesAsync();
        
        return Ok(addressUpdated);
    }
    
    [HttpPost("{orderId:guid}/dispatch")]
    public async Task<IActionResult> Dispatch([FromRoute] Guid orderId)
    {
        var orderDispatched = new Events.OrderDispatched
        {
            Id = orderId,
            DispatchedAt = DateTime.UtcNow
        };
        
        await using var session = _store.LightweightSession();
        session.Events.Append(orderId, orderDispatched);
        await session.SaveChangesAsync();
        
        return Ok(orderDispatched);
    }
    
    [HttpPost("{orderId:guid}/out-for-delivery")]
    public async Task<IActionResult> OutForDelivery([FromRoute] Guid orderId)
    {
        var orderOutForDelivery = new Events.OrderOutForDeliveryAt
        {
            Id = orderId,
            OutForDeliveryAt = DateTime.UtcNow
        };
        
        await using var session = _store.LightweightSession();
        session.Events.Append(orderId, orderOutForDelivery);
        await session.SaveChangesAsync();
        
        return Ok(orderOutForDelivery);
    }
    
    [HttpPost("{orderId:guid}/delivered")]
    public async Task<IActionResult> Delivered([FromRoute] Guid orderId)
    {
        var orderDelivered = new Events.OrderDelivered
        {
            Id = orderId,
            DeliveredAt = DateTime.UtcNow
        };
        
        await using var session = _store.LightweightSession();
        session.Events.Append(orderId, orderDelivered);
        await session.SaveChangesAsync();
        
        return Ok(orderDelivered);
    }
}