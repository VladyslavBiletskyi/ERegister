using System;
using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class AttendControlsRepository: IRepository<AttendControl>
    {
        private IDataContext context;

        public AttendControlsRepository(IDataContext context)
        {
            this.context = context;
        }

        public void Add(AttendControl entity)
        {
            context.Set<AttendControl>().Add(entity);
        }

        public void Remove(AttendControl entity)
        {
            throw new InvalidOperationException("You cant remove attends!");
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IQueryable<AttendControl> GetAll()
        {
            return context.Set<AttendControl>().AsQueryable();
        }
    }
}
