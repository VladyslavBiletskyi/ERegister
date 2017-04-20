namespace ERegister.DAL.Models
{
    public class Attend
    {
        public int Id { get; set; }
        public virtual AttendControl AttendControl { get; set; }
        public virtual ApplicationUser Student { get; set; }
    }
}