using Food_Mania.Models.ApplicationContext;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Repository.Interface;

namespace Food_Mania.Models.Repository.Implementation
{
    public class BaseRepository<T>: IBaseRepository<T> where T : BaseEntity
    {
       protected  Context _context;
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}