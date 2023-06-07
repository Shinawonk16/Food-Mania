using Food_Mania.Models.Entities;
using Food_Mania.Models.Repository.Interface;

namespace Models.Repository.Interface
{
    public interface IFoodOrderRepository : IBaseRepository<FoodOrder>
    {
        IEnumerable<FoodOrder> GetAllCustomers();
    }
}