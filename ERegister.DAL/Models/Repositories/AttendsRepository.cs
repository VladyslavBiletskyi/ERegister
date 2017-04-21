using System;
using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class AttendsRepository: IRepository<Attend>
    {
        private IDataContext context;

        public AttendsRepository(IDataContext context)
        {
            this.context = context;
        }

        public void Add(Attend entity)
        {
            context.Set<Attend>().Add(entity);
        }

        public void Remove(Attend entity)
        {
            throw new InvalidOperationException("You cant remove attends!");
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IQueryable<Attend> GetAll()
        {
            return context.Set<Attend>().AsQueryable();
        }
    }
}
