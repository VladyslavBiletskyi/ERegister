using System.Linq;

namespace ERegister.DAL.Models.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Remove(T entity);
        void SaveChanges();
        IQueryable<T> GetAll();
    }
}