using Azure;
using MediatR;
using Order.Application.Dtos;
using Order.Domain.OrderAggregate;

namespace Order.Application.Command;

public class CreateOrderCommand :IRequest<Microservices.Shared.Dtos.Response<CreatedOrderDto>>
{
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public AddressDto Address { get; set; }
    
    
}