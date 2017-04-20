using System.Data.Entity;
using ERegister.DAL.Models.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ERegister.DAL.Models
{
    public class ERegisterDbContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public ERegisterDbContext() : base("ERegisterDB", throwIfV1Schema: false)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Attend> Attends { get; set; }
        public DbSet<AttendControl> AttendControls { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<SubjectOfTheGroup> SubjectsOfGroups { get; set; }

        public static ERegisterDbContext Create()
        {
            return new ERegisterDbContext();
        }
    }
}