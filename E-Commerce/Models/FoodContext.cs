using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class FoodContext : IdentityDbContext<ApplicationUser>
    {
// لماذا هذا ضروري؟
// تمرير  إعدادات الاتصال: الـ DbContextOptions يحتوي على كل المعلومات المطلوبة للاتصال بقاعدة 
// البيانات(مثل connection string، وإعدادات الكاش، وتفاصيل أخرى)
        public FoodContext(DbContextOptions<FoodContext> options) : base(options) { }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-I7PU4G3;Database=FreshMarket;Trusted_Connection=True;Encrypt=false");
        }
    }
}
