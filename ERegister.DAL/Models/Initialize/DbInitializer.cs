using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ERegister.DAL.Models.Initialize
{
    public class DbInitializer:DropCreateDatabaseAlways<ERegisterDbContext>
    {
        protected override void Seed(ERegisterDbContext context)
        {
            context.Set<Group>().Add(new Group
            {
                Name = "SE-14-5"
            });
            context.Set<IdentityRole>().Add(new IdentityRole
            {
                Id = "Student",
                Name = "Student"
            });
            context.SaveChanges();
        }
    }
}