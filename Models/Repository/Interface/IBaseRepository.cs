namespace Food_Mania.Models.Repository.Interface
{
    public interface IBaseRepository<T>
    {
        T Create(T entity);
        T Update(T entity);
        int Save();
    }
}