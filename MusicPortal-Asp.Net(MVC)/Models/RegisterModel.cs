using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.Models
{
    // класс модели-представления (view-model) интересно для подготовки такой вьюшки что нас интересует 
    public class RegisterModel
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]//указываем регулярное выражение
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]//пароль вводится в закрытом виде 
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]//сравнивает пароль и подтверждение 
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}