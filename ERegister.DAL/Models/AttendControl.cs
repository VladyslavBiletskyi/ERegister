using System.Collections.Generic;

namespace ERegister.DAL.Models
{
    public class AttendControl : BaseEntity
    {
        public virtual Lesson Lesson { get; set; }
        public int ControllerId { get; set; }
        public virtual ICollection<Attend> Attends { get; set; }
    }
}