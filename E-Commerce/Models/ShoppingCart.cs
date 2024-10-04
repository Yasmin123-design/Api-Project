namespace E_Commerce.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<ShoppingCartItems>? Items { get; set; }
    }
}
