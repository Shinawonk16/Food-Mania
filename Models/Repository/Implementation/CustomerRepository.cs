using System.Linq.Expressions;
using Food_Mania.Models.ApplicationContext;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Food_Mania.Models.Repository.Implementation
{
    public class CustomerRepository: BaseRepository<Customer>, ICustomerRepository
    {
         public CustomerRepository(Context context)
        {
           _context = context;
        }
        public bool CheckIfEmailExist(string email)
        {
            return _context.Customers
            .Include(c => c.User)
            .Where(x => x.User.Email == email).Any();
        }

        public bool CheckIfExist(string email)
        {
            var find = _context.Users
            .Where(x => x.Email == email).Any();
            return find;
        }

        public bool Delete(Staff staff)
        {
            throw new NotImplementedException();
        }

        public Customer Get(string id)
        {
            return _context.Customers
            .Include(a => a.User)
            .FirstOrDefault(a => a.User.Id == id && a.IsDeleted == false);
        }

        public Customer Get(Expression<Func<Customer, bool>> expression)
        {
           return _context.Customers
            .Include(a => a.User)
            .FirstOrDefault(expression);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers
            .Include(a => a.User)
            .Where(a => a.IsDeleted == false)
            .ToList();
        }

        public IEnumerable<Customer> GetSelected(Expression<Func<Customer, bool>> expression)
        {
            return _context.Customers
            .Where(expression)
            .ToList();
        }

        public IEnumerable<Customer> GetSelected(string city)
        {
            return _context.Customers
            .Where(a => city.Contains(a.User.Address.City) && a.IsDeleted == false)
            .ToList();
        }
    }
}