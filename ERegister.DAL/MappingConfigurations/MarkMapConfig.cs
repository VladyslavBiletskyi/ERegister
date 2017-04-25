using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;

namespace ERegister.DAL.MappingConfigurations
{
    public class MarkMapConfig : BaseEntityConfiguration<Mark>
    {
    public MarkMapConfig()
    {
        HasRequired(x => x.Student).WithOptional().WillCascadeOnDelete(false);
        HasRequired(x => x.Teacher).WithOptional().WillCascadeOnDelete(false);
        HasRequired(x=>x.Lesson).WithMany(x=>x.Marks).WillCascadeOnDelete(false);
    }
}

}
