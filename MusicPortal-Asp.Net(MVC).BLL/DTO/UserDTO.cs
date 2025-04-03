using System.ComponentModel.DataAnnotations;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;

namespace MusicPortal_Asp.Net_MVC_.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс TeamDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Salt { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Role { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public bool IsActive { get; set; }
    }
}
