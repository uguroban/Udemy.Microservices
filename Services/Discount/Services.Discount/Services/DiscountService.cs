using System.Data;
using Dapper;
using Microservices.Shared.Dtos;
using Npgsql;

namespace Services.Discount.Services;

public class DiscountService : IDiscountService
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public DiscountService(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresSql"));
    }

    public async Task<Response<List<Models.Discount>>> GetAll()
    {
        var discounts = await _dbConnection.QueryAsync<Models.Discount>($"select * from discount");
        return Response<List<Models.Discount>>.Success(discounts.ToList(),200);
    }

    public async Task<Response<Models.Discount>> GetById(int id)
    {
        var discount =
            (await _dbConnection.QueryAsync<Models.Discount>($"select * from discount where Id=@Id", new { Id = id }))
            .SingleOrDefault();
        return discount == null
            ? Response<Models.Discount>.Fail("Discount not found", 404)
            : Response<Models.Discount>.Success(discount, 200);
    }

    public async Task<Response<NoContent>> Save(Models.Discount discount)
    {
        var saveStatus = await _dbConnection.ExecuteAsync(
            "insert into discount(userid,rate,code) values(@UserId,@Rate,@Code)",
            discount);
        return saveStatus > 0
            ? Response<NoContent>.Success(204)
            : Response<NoContent>.Fail("An error occured while adding", 500);

    }

    public async Task<Response<NoContent>> Update(Models.Discount discount)
    {
        var updateStatus = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,rate=@Rate,code=@Code where id=@id", new
        {
            Id = discount.Id,
            UserId = discount.UserId,
            Rate = discount.Rate,
            Code = discount.Code

        });

        return updateStatus > 0
            ? Response<NoContent>.Success(204)
            : Response<NoContent>.Fail("An error occured while updating", 500);
    }

    public async Task<Response<NoContent>> Delete(int id)
    {
        var discount =
            (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@id", new { Id = id })).SingleOrDefault();
        if (discount == null) return Response<NoContent>.Fail("Discount not found", 404);
        var deleteStatus = await _dbConnection.ExecuteAsync("delete from discount where id=@id", new { Id = id });
        return deleteStatus > 0
            ? Response<NoContent>.Success(200)
            : Response<NoContent>.Fail("An error occured while deleting", 500);
        }

    public async Task<Response<Models.Discount>> GetByCode(string code)
    {
        var discount = (await _dbConnection.QueryAsync<Models.Discount>(
            "select * from discount where code=@Code", new { Code = code })).SingleOrDefault();

            return discount == null
            ? Response<Models.Discount>.Fail("Discount not found", 404)
            : Response<Models.Discount>.Success(discount, 200);
    }
}