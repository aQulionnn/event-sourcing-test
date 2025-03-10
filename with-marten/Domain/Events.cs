namespace with_marten;

public class Events
{
    public class OrderCreated
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; }
        public string DeliveryAddress { get; set; }
    }

    public class OrderAddressUpdated
    {
        public Guid Id { get; set; }
        public string DeliveryAddress { get; set; }
    }

    public class OrderDispatched
    {
        public Guid Id { get; set; }
        public DateTime DispatchedAt { get; set; }
    }
    
    public class OrderOutForDeliveryAt
    {
        public Guid Id { get; set; }
        public DateTime OutForDeliveryAt { get; set; }
    }

    public class OrderDelivered
    {
        public Guid Id { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
}