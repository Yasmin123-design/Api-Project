using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_Commerce.Models
{
    public class ShoppingCartItemsRepository : IShoppingCartItems
    {
        private readonly FoodContext _context;
        public ShoppingCartItemsRepository(FoodContext context)
        {
            _context = context;
        }
        public void AddToCart(Food food)
        {
            var one = _context.ShoppingCartItems.Where(x => x.FoodId == food.Id).FirstOrDefault();
            if ( one == null)
            {
                ShoppingCartItems item = new ShoppingCartItems()
                {                  
                    Ammount = 1,
                    FoodId = food.Id,
                    FoodName = food.Name
                };
                _context.ShoppingCartItems.Add(item);
            }
            else
            {
                one.Ammount += 1;
            }
            _context.SaveChanges();
            
        }

        public void RemoveFromCart(Food food)
        {
            
            var one = _context.ShoppingCartItems.Where(x => x.FoodId == food.Id).FirstOrDefault();
            if(one != null)
            {
                if(one.Ammount > 1)
                {
                    one.Ammount -= 1;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(one);
                }
            }
            _context.SaveChanges();
        }
        public List<ShoppingCartItems> All()
        {
            return _context.ShoppingCartItems.ToList();
        }

        public void Clear()
        {
            var items = _context.ShoppingCartItems.ToList(); // تحميل جميع العناصر
            _context.ShoppingCartItems.RemoveRange(items); // حذف جميع العناصر
            _context.SaveChanges();
        }
    }
}
