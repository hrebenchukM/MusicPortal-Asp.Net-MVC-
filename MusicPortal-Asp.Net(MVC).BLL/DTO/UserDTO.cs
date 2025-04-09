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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
    }
}
