using Food_Mania.Models.Entities;

namespace Food_Mania.Models.Repository.Interface
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        bool Insert(FoodOrder foodOrder);
        IEnumerable<FoodOrder> Orders();
        Order Get(string id);
        
    }
}