using Food_Mania.Models.ApplicationContext;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Repository.Implementation;
using Models.Repository.Interface;

namespace Models.Repository.Implementation
{
    public class FoodOrderRepository : BaseRepository<FoodOrder>, IFoodOrderRepository
    {
        public FoodOrderRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<FoodOrder> GetAllCustomers()
        {
            return _context.FoodOrder
          .Where(a => a.IsDeleted == false)
          .ToList();
        }
    }
}