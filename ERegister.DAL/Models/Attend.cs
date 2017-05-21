namespace ERegister.DAL.Models
{
    public class Attend:BaseEntity
    {
        public virtual Lesson Lesson { get; set; }
        public virtual ApplicationUser Student { get; set; }
    }
}