using System;
using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class AttendsRepository: BaseRepository<Attend>,IAttendsRepository
    {       
        public AttendsRepository(IDataContext context):base(context)
        {
        }       
        public new void Remove(Attend entity)
        {
            throw new InvalidOperationException("You cant remove attends!");
        }        
    }
}
