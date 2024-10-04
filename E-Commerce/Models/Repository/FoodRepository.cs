
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodContext _context;
        public FoodRepository(FoodContext context)
        {
            _context = context;
        }
        public void AddNew(Food food)
        {
            _context.Add(food);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Food food = GetById(id);
            _context.Foods.Remove(food);
            _context.SaveChanges();

        }

        public List<Food> GetAll()
        {
            return _context.Foods.ToList();
        }

        public Food GetById(int id)
        {
            return _context.Foods.Where(x => x.Id == id).FirstOrDefault();
            
        }

        public Food GetWithCategoryName(int id)
        {
            return _context.Foods.Include(x => x.Category).Where(x => x.Id == id).FirstOrDefault();
            
        }

        public void Update(int id,Food food)
        {
            Food food1 = GetById(id);
            food1.Name = food.Name;
            food1.Description = food.Description;
            food1.Price = food.Price;
            food1.CategoryId = food.CategoryId;
            _context.SaveChanges();
        }
    }
}
