using System.ComponentModel.DataAnnotations;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;

namespace MusicPortal_Asp.Net_MVC_.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс PlayerDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class SongDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public int Year { get; set; }

        public string? PathS { get; set; }
        public string? PathV { get; set; }
        public string? PathP { get; set; }

        public int? ArtistId { get; set; }

        public int? GenreId { get; set; }
        public int? UserId { get; set; }
        public string? Artist { get; set; }
        public string? Genre { get; set; }
        public string? User { get; set; }
    }
}
