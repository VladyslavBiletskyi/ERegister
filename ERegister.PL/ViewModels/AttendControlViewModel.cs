using System.ComponentModel.DataAnnotations;

namespace ERegister.PL.ViewModels
{
    public class AttendControlViewModel
    {
        [Required]
        public int ControllerId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}