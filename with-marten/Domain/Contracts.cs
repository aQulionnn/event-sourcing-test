namespace with_marten;

public record CreateOrderRequest(string ProductName, string DeliveryAddress);
    
public record DeliveryAddressUpdateRequest(string DeliveryAddress);
