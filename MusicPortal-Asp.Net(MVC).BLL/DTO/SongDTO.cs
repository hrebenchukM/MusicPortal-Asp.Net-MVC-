using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс PlayerDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class SongDTO
    {
        public int Id { get; set; }
        //все указываем идентификаторами а не поле должно быть заполнено
        //сообщение об ошибки будет браться из файлов ресурсов по идентификатору
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "TitleRequired")]
        [Display(Name = "Title", ResourceType = typeof(Resources.Resource))]
        public string? Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
             ErrorMessageResourceName = "YearRequired")]
        [Display(Name = "Year", ResourceType = typeof(Resources.Resource))]
        public int Year { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "PathSRequired")]
        [Display(Name = "PathS", ResourceType = typeof(Resources.Resource))]
        public string? PathS { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "PathVRequired")]
        [Display(Name = "PathV", ResourceType = typeof(Resources.Resource))]

        public string? PathV { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "PathPRequired")]
        [Display(Name = "PathP", ResourceType = typeof(Resources.Resource))]
        public string? PathP { get; set; }

        public int? ArtistId { get; set; }

        public int? GenreId { get; set; }
        public int? UserId { get; set; }
        public string? Artist { get; set; }
        public string? Genre { get; set; }
        public string? User { get; set; }
    }
}
