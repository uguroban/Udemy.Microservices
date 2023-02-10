namespace Services.Basket.Dtos;

public class BasketDto
{
    private string? _userId;
    private string? _discountCode;

    public string? UserId
    {
        get => _userId;
        set => _userId = value;
    }

    public string? DiscountCode
    {
        get => _discountCode;
        set => _discountCode = value;
    }

    public List<BasketItemDto>? BasketItems { get; set; }

    public decimal TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);
}