using System.Linq;

namespace ERegister.DAL.Models.Interfaces
{
    public interface IRepository<T>
    {
        IDataContext GetContext();
        void Add(T entity);
        void Remove(T entity);
        void SaveChanges();
        IQueryable<T> GetAll();
    }
}