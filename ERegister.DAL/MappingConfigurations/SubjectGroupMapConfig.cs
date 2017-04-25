using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;

namespace ERegister.DAL.MappingConfigurations
{
    public class SubjectGroupMapConfig : BaseEntityConfiguration<SubjectOfTheGroup>
    {
    public SubjectGroupMapConfig()
    {
        HasRequired(x => x.Teacher).WithOptional().WillCascadeOnDelete(false);
        HasRequired(x => x.Subject).WithOptional().WillCascadeOnDelete(false);
    }
}

}
