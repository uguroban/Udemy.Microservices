using Order.Domain.OrderAggregate;

namespace Order.Application.Dtos;

public class OrderDto
{
    public DateTime CreatedAt { get; set; }
    public AddressDto Address { get; set; }
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}