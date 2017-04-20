namespace ERegister.DAL.Models
{
    public class SubjectOfTheGroup
    {
        public int Id { get; set; }
        public virtual Group Group { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
    }
}