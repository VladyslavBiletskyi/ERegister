using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class SubjectsRepository : BaseRepository<Subject>, ISubjectsRepository
    {
        public SubjectsRepository(IDataContext context):base(context)
        {
        }
    }
}
