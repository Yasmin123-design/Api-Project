namespace E_Commerce.Models
{
    public interface IFoodRepository
    {
        void AddNew(Food food);
        void Update(int id,Food food);
        void Delete(int id);
        List<Food> GetAll();
        Food GetById(int id);
        Food GetWithCategoryName(int id);

    }
}
