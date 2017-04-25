using System.Data.Entity.ModelConfiguration;
using ERegister.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ERegister.DAL.MappingConfigurations
{
    public class UserLoginMapConfig : EntityTypeConfiguration<IdentityUserLogin>
{
    public UserLoginMapConfig()
    {
        HasKey(x => x.UserId);
    }
}

}
