using System.ComponentModel.DataAnnotations;

namespace ERegister.ViewModels
{
    public class ChangeUserInfoModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "фамилия пользователя")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Группа")]
        public string Group { get; set; }

    }
}