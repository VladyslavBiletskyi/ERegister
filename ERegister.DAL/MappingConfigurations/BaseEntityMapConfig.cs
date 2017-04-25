using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;

namespace ERegister.DAL.MappingConfigurations
{
    public abstract class BaseEntityConfiguration<TEntityType> : EntityTypeConfiguration<TEntityType>
        where TEntityType : BaseEntity
    {
        protected BaseEntityConfiguration()
        {
            Property(be => be.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
