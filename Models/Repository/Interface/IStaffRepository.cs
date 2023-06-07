using System.Linq.Expressions;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Enum;

namespace Food_Mania.Models.Repository.Interface
{
    public interface IStaffRepository:IBaseRepository<Staff>
    {
        bool CheckIfEmailExist(string email);
        Staff GetById(string id);
        bool Delete(Staff staff);
        Staff Get( Expression<Func<Staff ,bool>> expression);
        IEnumerable<Staff> GetSelected(Expression<Func<Staff, bool>> expression);
        Staff GetByRole(Role role);
        IEnumerable<Staff> GetAllStaff();
    }
}