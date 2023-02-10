using System.Text.Json;
using Microservices.Shared.Dtos;
using Services.Basket.Dtos;

namespace Services.Basket.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<Response<BasketDto>> GetBasket(string userId)
    {
        var exisBasket = await _redisService.GetDb().StringGetAsync(userId);
        if (String.IsNullOrEmpty(exisBasket))
        {
            return Response<BasketDto>.Fail("Basket not found", 404);
        }

        return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(exisBasket), 200);
    }

    public async Task<Response<bool>> SaveorUpdate(BasketDto basketDto)
    {
        var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

        return status ? Response<bool>.Success(204) : Response<bool>.Fail("Update or Save fail", 500);
    }

    public async Task<Response<bool>> Delete(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
        
        return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 404);

    }
}