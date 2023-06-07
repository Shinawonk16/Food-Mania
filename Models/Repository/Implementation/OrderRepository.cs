using Food_Mania.Models.ApplicationContext;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Food_Mania.Models.Repository.Implementation
{
    public class OrderRepository : BaseRepository<Order> ,IOrderRepository
    {
         public OrderRepository(Context context)
        {
           _context = context;
        }
        public Order Get(string id)
        {
            return _context.Orders
            .Include(c => c.Address)
            .Include(c => c.Customer)
            .ThenInclude(c => c.User)
            .Where(x => x.Id == id && x.IsDeleted == false)
            
            .SingleOrDefault();
        }

        public bool Insert(FoodOrder foodOrder)
        {
            _context.FoodOrder.Add(foodOrder);
           _context.SaveChanges();
            return true;
        }

        public IEnumerable<FoodOrder> Orders()
        {
            var order = _context.FoodOrder
                                    .Include(c => c.Food)
                                    .Include(x => x.Order)
                                    .Include(c => c.Order.Address)
                                    .Include(y => y.Order.Customer)
                                    .ThenInclude(z => z.User)
                                    .ToList();

            return order;
        }
    }
}