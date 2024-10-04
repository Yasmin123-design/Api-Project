using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FoodContext _context;
        public CategoryRepository(FoodContext context)
        {
            _context = context;
        }
        public void AddNew(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Category category = GetById(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();

        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Where(x => x.Id == id).FirstOrDefault();

        }

        public void Update(int id, Category category)
        {
            Category category1 = GetById(id);
            category1.Name = category.Name;
            _context.SaveChanges();
        }
    }

}
