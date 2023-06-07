using System.Linq.Expressions;
using Food_Mania.Models.ApplicationContext;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Enum;
using Food_Mania.Models.Repository.Implementation;
using Food_Mania.Models.Repository.Interface;

namespace Models.Repository.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly Context context ;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public bool CheckIfExist(string email)
        {
            
            var find = _context.Users
            .Where(x => x.Email == email).Any();
            return find;
        }

        public User Get(string id)
        {
            var get = _context.Users
            .Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefault();
            return get;
        }

        public User Get(Expression<Func<User, bool>> expression)
        {
            return _context.Users
            .FirstOrDefault(expression);
        }

        public IEnumerable<User> GetSelected(Expression<Func<User, bool>> expression)
        {
            return _context.Users
            .Where(expression)
            .ToList();
        }

        public User GetUserByRole(Role role)
        {
            return  _context.Users
            .SingleOrDefault(x => x.Role == role && x.IsDeleted == false);
        }

        public IEnumerable<User> GetUsers()
        {
           return _context.Users
            .Where(a => a.IsDeleted == false)
            .ToList();
        }

        public User Login(string email, string password)
        {
            return _context.Users
            .Where(a => a.Email == email && a.Password == password && a.IsDeleted == false)
            .SingleOrDefault();
        }
    }
}