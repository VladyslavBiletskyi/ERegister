using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class GroupsRepository:IRepository<Group>
    {
        private IDataContext context;

        public GroupsRepository(IDataContext context)
        {
            this.context = context;
        }

        public void Add(Group entity)
        {
            context.Set<Group>().Add(entity);
        }

        public void Remove(Group entity)
        {
            context.Set<Group>().Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IQueryable<Group> GetAll()
        {
            return context.Set<Group>().AsQueryable();
        }
    }
}