namespace E_Commerce.Models
{
    public class Order
    {

        public int Id { get; set; }
        public List<OrderItem>? Items = new List<OrderItem>();
        public string FName { get; set; }
        public string LName { get; set; }
        public string City { get; set; }
        public DateTime? OrderedPlaced { get; set; }
    }
}
