using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class LessonsRepository:BaseRepository<Lesson>,ILessonsRepository
    {
        public LessonsRepository(IDataContext context):base(context)
        {
        }
    }
}
