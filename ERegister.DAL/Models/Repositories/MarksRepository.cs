using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class MarksRepository:IRepository<Mark>
    {
        private IDataContext context;

        public MarksRepository(IDataContext context)
        {
            this.context = context;
        }

        public void Add(Mark entity)
        {
            context.Set<Mark>().Add(entity);
        }

        public void Remove(Mark entity)
        {
            context.Set<Mark>().Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IQueryable<Mark> GetAll()
        {
            return context.Set<Mark>().AsQueryable();
        }
    }
}