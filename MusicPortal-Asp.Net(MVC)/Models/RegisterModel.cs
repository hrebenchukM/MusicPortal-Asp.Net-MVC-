using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.Models
{
    // класс модели-представления (view-model) интересно для подготовки такой вьюшки что нас интересует 
    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "FirstNameRequired")]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        public string? FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                   ErrorMessageResourceName = "LastNameRequired")]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        public string? LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                     ErrorMessageResourceName = "LoginRequired")]
        [Display(Name = "Login", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                   ErrorMessageResourceType = typeof(Resources.Resource),
                   ErrorMessageResourceName = "InvalidEmailAddress")]//указываем регулярное выражение
        public string? Login { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
              ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]

        [DataType(DataType.Password)]//пароль вводится в закрытом виде 
        public string? Password { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
              ErrorMessageResourceName = "PasswordConfirmRequired")]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(Resources.Resource))]

        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource),
         ErrorMessageResourceName = "PasswordsDoNotMatch")]//сравнивает пароль и подтверждение 

        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}