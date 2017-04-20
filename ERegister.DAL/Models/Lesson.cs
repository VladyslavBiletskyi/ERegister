using System;

namespace ERegister.DAL.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public virtual SubjectOfTheGroup Subject { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
        public DateTime BeginigDateTime { get; set; }
        public string Room { get; set; }
    }
}