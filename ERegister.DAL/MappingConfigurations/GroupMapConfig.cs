using ERegister.DAL.Models;

namespace ERegister.DAL.MappingConfigurations
{
    public class GroupMapConfig : BaseEntityConfiguration<Group>
    {
    public GroupMapConfig()
    {
        HasMany(x => x.Students).WithOptional(x=>x.Group).WillCascadeOnDelete(false);
        HasOptional(x => x.Previous);
        HasMany(x=>x.GroupSubjects).WithRequired(x=>x.Group).WillCascadeOnDelete(false);
    }
}

}
