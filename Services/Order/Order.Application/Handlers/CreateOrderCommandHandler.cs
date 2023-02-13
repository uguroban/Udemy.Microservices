using MediatR;
using Microservices.Shared.Dtos;
using Order.Application.Command;
using Order.Application.Dtos;
using Order.Application.Mapping;
using Order.Domain.OrderAggregate;
using Order.Infrastructure;

namespace Order.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,Response<CreatedOrderDto>>
{
    private readonly OrderDbContext _orderDbContext;

    public CreateOrderCommandHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }

    public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street,
            request.Address.ZipCode, request.Address.Line);

        var newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);
        
        request.OrderItems.ForEach(x =>
        {
            newOrder.AddOrderItem(x.ProductId,x.ProductName,x.PictureUrl,x.Price);
        });

         await _orderDbContext.AddAsync(newOrder,cancellationToken);

         await _orderDbContext.SaveChangesAsync(cancellationToken);
         
        return Response<CreatedOrderDto>.Success(new CreatedOrderDto{OrderId = newOrder.Id} ,200);

    }
}