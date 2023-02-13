using MediatR;
using Microservices.Shared.Dtos;
using Order.Application.Dtos;

namespace Order.Application.Queries;

public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
{
    public string UserId { get; set; }
}