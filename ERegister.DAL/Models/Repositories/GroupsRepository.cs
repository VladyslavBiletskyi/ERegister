using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class GroupsRepository:BaseRepository<Group>,IGroupsRepository
    {
        public GroupsRepository(IDataContext context):base(context)
        {
        }
    }
}