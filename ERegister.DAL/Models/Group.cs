using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERegister.DAL.Models
{
    public class Group : BaseEntity
    {
        [Required(ErrorMessage = "Define the name of the group")]
        public string Name { get; set; }
        public virtual Group Previous { get; set; }
        public virtual ICollection<SubjectOfTheGroup> GroupSubjects { get; set; }
        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}