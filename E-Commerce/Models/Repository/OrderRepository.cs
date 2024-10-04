using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodContext context;
        private readonly IShoppingCartItems cartItems;
        public OrderRepository(FoodContext _context , IShoppingCartItems _cartItems)
        {
            context = _context;
            cartItems = _cartItems;
        }

        public void CreateOrder(Order order)
        {
            
             
            foreach(var item in cartItems.All())
            {
                OrderItem orderItem = new OrderItem()
                {
                    Amount = item.Ammount,
                    FoodId = item.FoodId,
                    Name = item.FoodName
                };
                order.Items.Add(orderItem);            
            }            
            context.Orders.Add(order);
            context.SaveChanges();            
        }
    }
}
