using ERegister.DAL.Models;

namespace ERegister.DAL.MappingConfigurations
{
    public class SubjectGroupMapConfig : BaseEntityConfiguration<SubjectOfTheGroup>
    {
        public SubjectGroupMapConfig()
        {
            HasMany(x => x.Lessons).WithRequired(x => x.Subject).WillCascadeOnDelete(false);
            HasRequired(x => x.Teacher).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Subject).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Group).WithMany(x => x.GroupSubjects).WillCascadeOnDelete(false);
        }
    }

}
