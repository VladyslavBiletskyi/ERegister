namespace ERegister.DAL.Models
{
    public class AttendControl
    {
        public int Id { get; set; }
        public virtual Lesson Lesson { get; set; }
        public int ControllerId { get; set; }
    }
}