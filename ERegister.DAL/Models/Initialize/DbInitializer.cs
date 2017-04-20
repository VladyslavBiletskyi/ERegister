using System.Data.Entity;

namespace ERegister.DAL.Models.Initialize
{
    public class DbInitializer:DropCreateDatabaseIfModelChanges<ERegisterDbContext>
    {
        protected override void Seed(ERegisterDbContext context)
        {
            context.Groups.Add(new Group
            {
                Name = "SE-14-5"
            });
            context.SaveChanges();
        }
    }
}