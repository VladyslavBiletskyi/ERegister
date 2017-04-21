using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERegister.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "фамилия пользователя")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Группа")]
        public int Group { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}