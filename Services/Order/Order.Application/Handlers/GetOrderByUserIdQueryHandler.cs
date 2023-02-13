using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Queries;
using Order.Infrastructure;
using Microservices.Shared.Dtos;
using Order.Application.Dtos;
using Order.Application.Mapping;

namespace Order.Application.Handlers;

public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery,Response<List<OrderDto>>>
{
    private readonly OrderDbContext _orderDbContext;

    public GetOrderByUserIdQueryHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderDbContext.Orders.Include(x => x.OrderItems)
            .Where(x=>x.BuyerId==request.UserId).ToListAsync(cancellationToken);

        if (!orders.Any()) return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);

        var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
        
        return Response<List<OrderDto>>.Success(ordersDto, 200);

    }
    
}