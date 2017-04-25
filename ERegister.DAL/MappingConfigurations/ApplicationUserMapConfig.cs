using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ERegister.DAL.MappingConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("ApplicationUsers");
            });
            HasOptional(x => x.Group).WithMany(x => x.Students);
            Property(u => u.FirstName).IsRequired();
            Property(u => u.LastName).IsRequired();
        }
    }
}
