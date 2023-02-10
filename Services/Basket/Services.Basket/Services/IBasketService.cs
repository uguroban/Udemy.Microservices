using Microservices.Shared.Dtos;
using Services.Basket.Dtos;

namespace Services.Basket.Services;

public interface IBasketService
{
        Task<Response<BasketDto>> GetBasket(string userId);
    Task<Response<bool>> SaveorUpdate(BasketDto basketDto);
    Task<Response<bool>> Delete(string id);
}