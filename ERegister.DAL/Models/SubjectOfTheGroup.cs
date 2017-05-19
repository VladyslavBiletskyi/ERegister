using System.Collections.Generic;

namespace ERegister.DAL.Models
{
    public class SubjectOfTheGroup : BaseEntity
    {
        public virtual Group Group { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}