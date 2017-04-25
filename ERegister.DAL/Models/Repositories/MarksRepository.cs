using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class MarksRepository:BaseRepository<Mark>,IMarksRepository
    {
        public MarksRepository(IDataContext context):base(context)
        {
        }
    }
}