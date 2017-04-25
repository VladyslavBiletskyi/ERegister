using System.Data.Entity;
using System.Reflection;
using ERegister.DAL.Models.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ERegister.DAL.Models
{
    public class ERegisterDbContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        private static int id = 0;

        private int idObj = ++id;

        public ERegisterDbContext() : base("ERegisterDB", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(ERegisterDbContext)));
        }
        public static ERegisterDbContext Create()
        {
            return new ERegisterDbContext();
        }

    }
}