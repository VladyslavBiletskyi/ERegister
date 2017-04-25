using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;

namespace ERegister.DAL.MappingConfigurations
{
    public class LessonMapConfig : BaseEntityConfiguration<Lesson>
    {
    public LessonMapConfig()
    {
        HasRequired(x => x.Teacher).WithOptional().WillCascadeOnDelete(false);
        HasRequired(x => x.Subject).WithOptional().WillCascadeOnDelete(false);
    }
}

}
