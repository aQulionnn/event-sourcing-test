using Marten.Events.Aggregation;

namespace with_marten;

public class OrderProjection : SingleStreamProjection<Order>
{
    public void Apply(Events.OrderCreated created, Order order)
    {
        order.Id = created.Id;
        order.ProductName = created.ProductName;
        order.DeliveryAddress = created.DeliveryAddress;
    }

    public void Apply(Events.OrderAddressUpdated updated, Order order)
    {
        order.DeliveryAddress = updated.DeliveryAddress;
    }

    public void Apply(Events.OrderDispatched dispatched, Order order)
    {
        order.DispatchedAt = dispatched.DispatchedAt;
    }

    public void Apply(Events.OrderOutForDeliveryAt outForDelivery, Order order)
    {
        order.OutForDeliveryAt = outForDelivery.OutForDeliveryAt;
    }

    public void Apply(Events.OrderDelivered delivered, Order order)
    {
        order.DeliveredAt = delivered.DeliveredAt;
    }
}