using System.ComponentModel.DataAnnotations;

namespace ERegister.DAL.Models
{
    public class Subject
    {
        [Key]
        public string Name { get; set; }
    }
}