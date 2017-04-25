using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;

namespace ERegister.DAL.MappingConfigurations
{
    public class AttendControlMapConfig : BaseEntityConfiguration<AttendControl>

    {
        public AttendControlMapConfig()
        {
            HasMany(x => x.Attends).WithRequired(x=>x.AttendControl).WillCascadeOnDelete(true);
            HasRequired(x => x.Lesson).WithOptional().WillCascadeOnDelete(false);
        }
    }

}
