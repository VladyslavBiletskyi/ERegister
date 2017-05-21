using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;

namespace ERegister.DAL.MappingConfigurations
{
    public class AttendMapConfig : BaseEntityConfiguration<Attend>

    {
        public AttendMapConfig()
        {
            HasRequired(x => x.Student).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Lesson).WithMany(x => x.Attends).WillCascadeOnDelete(false);
        }
    }

}
