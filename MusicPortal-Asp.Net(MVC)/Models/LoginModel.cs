using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.Models
{
    // класс модели-представления (view-model)//по этим моделям не создаются таблицы в базе данных . Таблица в бд создается только по доменной модели юзерс
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]//указываем регулярное выражение
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]//пароль вводится в закрытом виде 
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}