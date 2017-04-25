using System;
using System.Linq;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.DAL.Models.Repositories
{
    public class AttendControlsRepository: BaseRepository<AttendControl>,IAttendControlsRepository
    {
        public AttendControlsRepository(IDataContext context):base(context)
        {
        }
    }
}
