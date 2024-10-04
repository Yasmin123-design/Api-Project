namespace E_Commerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Food>? Foods { get; set; }
    }
}
