using Order.Domain.Core;

namespace Order.Domain.OrderAggregate;

public class Order :Entity,IAggregateRoot
{
    public DateTime CreatedAt { get;private set; }
    public Address Address { get;private set; }
    public string BuyerId { get;private set; }
    
    //OrderItem'ların eklenmesini dış dünyaya kapatıyoruz(Encapsulating)
    private readonly List<OrderItem> _orderItems;

    //OrderItem'ları sadece okuma yapabilecek şekilde dış dünyaya açıyoruz(dışardan set edilemez)
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Order()
    {
        
    }
    public Order(string buyerId,Address address)
    {
        _orderItems = new List<OrderItem>();
        CreatedAt = DateTime.Now;
        BuyerId = buyerId;
        Address = address;
    }

    public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
    {
        var existProduct =  _orderItems.Any(x => x.ProductId == productId);

        if (existProduct) return;
        var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
        _orderItems.Add(newOrderItem);
    }

    //Lambda ile kullandığımız için bu property'nin sadece get'i var
    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);

}