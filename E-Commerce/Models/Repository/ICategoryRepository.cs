namespace E_Commerce.Models
{
    public interface ICategoryRepository
    {
        void AddNew(Category category);
        void Update(int id,Category category);
        void Delete(int id);
        List<Category> GetAll();
        Category GetById(int id);

    }
}
