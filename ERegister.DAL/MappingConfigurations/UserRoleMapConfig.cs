using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ERegister.DAL.MappingConfigurations
{
    public class UserRoleMapConfig : EntityTypeConfiguration<IdentityUserRole>
{
    public UserRoleMapConfig()
    {
        HasKey(x => new {x.RoleId, x.UserId});
    }
}

}
