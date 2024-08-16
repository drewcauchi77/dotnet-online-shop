namespace OnlineShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnlineShopDbContext _onlineShopDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(OnlineShopDbContext onlineShopDbContext, IShoppingCart shoppingCart)
        {
            _onlineShopDbContext = onlineShopDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();
            // Adding the order with its details
            foreach (ShoppingCartItem shoppingCartItem in shoppingCartItems) 
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _onlineShopDbContext.Orders.Add(order);
            _onlineShopDbContext.SaveChanges();
        }
    }
}
