using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ERegister.DAL.MappingConfigurations
{
    public class RoleMapConfig : EntityTypeConfiguration<IdentityRole>
{
    public RoleMapConfig()
    {
        HasKey(x => x.Id);
    }
}

}
