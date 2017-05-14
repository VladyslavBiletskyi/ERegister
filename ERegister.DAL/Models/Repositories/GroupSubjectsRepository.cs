using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class GroupSubjectsRepository : BaseRepository<SubjectOfTheGroup>, IGroupSubjectsRepository
    {
        public GroupSubjectsRepository(IDataContext context):base(context)
        {
        }
    }
}
