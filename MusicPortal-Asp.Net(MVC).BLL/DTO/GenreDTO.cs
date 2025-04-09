using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс TeamDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class GenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
               ErrorMessageResourceName = "NameGRequired")]
        [Display(Name = "NameG", ResourceType = typeof(Resources.Resource))]
        public string? Name { get; set; }
    }
}
