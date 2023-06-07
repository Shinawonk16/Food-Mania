using System.Linq.Expressions;
using Food_Mania.Models.ApplicationContext;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Enum;
using Food_Mania.Models.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Food_Mania.Models.Repository.Implementation
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(Context context)
        {
           _context = context;
        }

        public bool CheckIfEmailExist(string email)
        {
            return _context.Staffs
            .Include(c => c.User)
            .Where(x => x.User.Email == email).Any();
        }

        public bool Delete(Staff staff)
        {
            throw new NotImplementedException();
        }

        public Staff Get(Expression<Func<Staff, bool>> expression)
        {
             return _context.Staffs
            .Include(a => a.User)
            .Where(a => a.IsDeleted == false)
            .FirstOrDefault(expression);
        }

        public IEnumerable<Staff> GetAllStaff()
        {
            return _context.Staffs
            .Include(a => a.User)
            .Where(a => a.IsDeleted == false)
            .ToList();
        }


        public Staff GetById(string id)
        {
            return _context.Staffs
            .Include(a => a.User)
            .FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public Staff GetByRole(Role role)
        {
            return  _context.Staffs
            .Include(c => c.User)
            .SingleOrDefault(x => x.User.Role == role);
        }

        public IEnumerable<Staff> GetSelected(Expression<Func<Staff, bool>> expression)
        {
             return _context.Staffs
            .Include(a => a.User)
            .Where(expression)
            .ToList();
        }
    }
}