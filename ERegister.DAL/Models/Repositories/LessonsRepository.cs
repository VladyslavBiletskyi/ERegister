using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class LessonsRepository:IRepository<Lesson>
    {
        IDataContext context;

        public LessonsRepository(IDataContext context)
        {
            this.context = context;
        }

        public void Add(Lesson entity)
        {
            context.Set<Lesson>().Add(entity);
        }

        public void Remove(Lesson entity)
        {
            context.Set<Lesson>().Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IQueryable<Lesson> GetAll()
        {
            return context.Set<Lesson>().AsQueryable();
        }
    }
}
