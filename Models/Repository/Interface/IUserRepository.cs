using System.Linq.Expressions;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Enum;

namespace Food_Mania.Models.Repository.Interface
{
    public interface IUserRepository:IBaseRepository<User>
    {
        User Get(string id);
        User Get( Expression<Func<User ,bool>> expression);
        IEnumerable<User> GetSelected(Expression<Func<User, bool>> expression);
        IEnumerable<User> GetUsers();
        User GetUserByRole(Role role);
        bool CheckIfExist(string email);
        User Login(string email,string password);
    }
}