namespace with_marten;

public class Order
{
    public Guid Id { get; set; }
    
    public string ProductName { get; set; }
    
    public string DeliveryAddress { get; set; }
    
    public DateTime? DispatchedAt { get; set; }
    
    public DateTime? OutForDeliveryAt { get; set; }
    
    public DateTime? DeliveredAt { get; set; }

    public void Apply(CreateOrderRequest request)
    {
        ProductName = request.ProductName;
        DeliveryAddress = request.DeliveryAddress;
    }

    public void Apply(Events.OrderDispatched dispatched)
    {
        DispatchedAt = dispatched.DispatchedAt;
    }

    public void Apply(Events.OrderOutForDeliveryAt outForDelivery)
    {
        OutForDeliveryAt = outForDelivery.OutForDeliveryAt;
    }

    public void Apply(Events.OrderDelivered delivered)
    {
        DeliveredAt = delivered.DeliveredAt;
    }

    public void Apply(Events.OrderAddressUpdated updated)
    {
        DeliveryAddress = updated.DeliveryAddress;
    }
}