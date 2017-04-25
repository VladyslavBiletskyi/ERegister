namespace ERegister.DAL.Models
{
    public class Attend:BaseEntity
    {
        public virtual AttendControl AttendControl { get; set; }
        public virtual ApplicationUser Student { get; set; }
    }
}