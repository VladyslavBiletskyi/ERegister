using System.ComponentModel.DataAnnotations.Schema;

namespace ERegister.DAL.Models
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}