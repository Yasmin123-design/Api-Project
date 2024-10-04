namespace E_Commerce.Models
{
    public interface IShoppingCartItems
    {
        void AddToCart(Food food);
        void RemoveFromCart(Food food);
        List<ShoppingCartItems> All();
        void Clear();

    }
}
