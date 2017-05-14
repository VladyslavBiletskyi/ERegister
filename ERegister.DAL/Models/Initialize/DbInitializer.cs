using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using ERegister.DAL.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace ERegister.DAL.Models.Initialize
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ERegisterDbContext>
    {
        protected override void Seed(ERegisterDbContext context)
        {
            context.Set<Group>()
                .Add(new Group
                {
                    Name = "Teachers"
                });
            context.Set<Group>()
                .Add(new Group
                {
                    Name = "SE-14-5"
                });
            context.Set<IdentityRole>()
                .Add(new IdentityRole
                {
                    Id = "Student",
                    Name = "Student"
                });
            context.Set<IdentityRole>()
                .Add(new IdentityRole
                {
                    Id = "Teacher",
                    Name = "Teacher"
                });
            context.SaveChanges();
            ApplicationUser teacher = new ApplicationUser
            {
                FirstName = "TeacherName",
                LastName = "NeacherLastName",
                };
            teacher.Roles.Add(new IdentityUserRole {RoleId = "Teacher", UserId = teacher.Id});
            var userManager =
                new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            userManager.Create(teacher, "Password");
        }
    }
}