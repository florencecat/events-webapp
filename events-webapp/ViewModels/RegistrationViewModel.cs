using System.ComponentModel.DataAnnotations;

namespace wa_dev_coursework.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}