using System.Linq.Expressions;
using Food_Mania.Models.Entities;

namespace Food_Mania.Models.Repository.Interface
{
    public interface ICustomerRepository: IBaseRepository<Customer>
    {
        bool CheckIfEmailExist(string email);
        Customer Get(string id);
        IEnumerable<Customer> GetAllCustomers();
        bool CheckIfExist(string email);
        bool Delete(Staff staff);
        Customer Get(Expression<Func<Customer, bool>> expression);
        IEnumerable<Customer> GetSelected(Expression<Func<Customer, bool>> expression);
        IEnumerable<Customer> GetSelected(string city);
    }
}