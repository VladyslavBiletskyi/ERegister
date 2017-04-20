using System.ComponentModel.DataAnnotations;

namespace ERegister.DAL.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public virtual ApplicationUser Student { get; set; }
        public virtual ApplicationUser Teacher { get; set; }

        public virtual Lesson Lesson { get; set; }
        [Range(0,100)]
        public int Result { get; set; }
    }
}