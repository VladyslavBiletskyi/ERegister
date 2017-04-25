using System.Data.Entity.Infrastructure;
using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public abstract class BaseRepository<T>:IRepository<T> where T:BaseEntity
    {
        protected IDataContext context;

        public BaseRepository(IDataContext context)
        {
            this.context = context;
        }

        public IDataContext GetContext()
        {
            return context;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>().AsQueryable();
        }
    }
}