using System.Linq.Expressions;
using Food_Mania.Models.ApplicationContext;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Food_Mania.Models.Repository.Implementation
{
    public class FoodRepository :BaseRepository<Food>, IFoodRepository
    {
         public FoodRepository(Context context)
        {
           _context = context;
        }
        public bool CheckIfExist(string type)
        {
            return  _context.Foods
            .Where(x => x.Type == type).Any();
        }

        public bool Delete(Food food)
        {
            throw new NotImplementedException();
        }

        public Food Get(string id)
        {
            return _context.Foods
            .Include(x => x.Categories)
            .FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public Food Get(Expression<Func<Food, bool>> expression)
        {
            return _context.Foods
            .Include(x => x.Categories)
            .SingleOrDefault(expression);
        }

        public IEnumerable<Food> GetAllFood()
        {
            return _context.Foods
            .Include(x => x.Categories)
            .Where(a => a.IsDeleted == false)
            .ToList();
        }

        public Food GetByPrice(decimal price)
        {
            return  _context.Foods
            .Where(x => x.Price >= price)
            .FirstOrDefault();
        }

        public IEnumerable<Food> GetFoodByCategory(string id)
        {
            return _context.Foods
            .Where(a => a.Categories.Id == id)
            .ToList();
        }

        public IEnumerable<Food> GetSelected(Expression<Func<Food, bool>> expression)
        {
            return _context.Foods
            .Where(expression)
            .ToList();
        }
    }
}