namespace OnlineShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
