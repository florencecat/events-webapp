using System.ComponentModel.DataAnnotations;

namespace wa_dev_coursework.ViewModels
{
    public class LoginViewModel
    {
        // Properties
        [Required]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Запомнить пользователя?")]
        public bool RememberUser { get; set; }
    }
}
