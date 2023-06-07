using System.Linq.Expressions;
using Food_Mania.Models.Entities;
using Models.Entities;

namespace Food_Mania.Models.Repository.Interface
{
    public interface IFoodRepository:IBaseRepository<Food>
    {
        Food Get(string id);
        Food Get(Expression<Func<Food, bool>> expression);
        IEnumerable<Food> GetSelected(Expression<Func<Food, bool>> expression);
        IEnumerable<Food> GetAllFood();
        bool CheckIfExist(string type);
        bool Delete(Food food);
        Food GetByPrice(decimal price);
       IEnumerable<Food> GetFoodByCategory(string id);
    }
}