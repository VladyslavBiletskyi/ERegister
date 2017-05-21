using ERegister.DAL.Models;

namespace ERegister.DAL.MappingConfigurations
{
    public class LessonMapConfig : BaseEntityConfiguration<Lesson>
    {
        public LessonMapConfig()
        {
            HasRequired(x => x.Teacher).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Subject).WithMany(x => x.Lessons).WillCascadeOnDelete(false);
            HasMany(x => x.Marks).WithRequired(x => x.Lesson).WillCascadeOnDelete(false);
            HasMany(x => x.Attends).WithRequired(x => x.Lesson).WillCascadeOnDelete(false);
        }
    }
}

