using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicPortal_Asp.Net_MVC_.Models
{
    public class FilterViewModel//для фильрации, нужно чтоб сохранить инфу которую мы выбрали, ибо при перезагрузке страницы данные теряются
    {
        public FilterViewModel(List<Genre> genres, int genre, string artist)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            genres.Insert(0, new Genre { Name = "All", Id = 0 });
            Genres = new SelectList(genres, "Id", "Name", genre);//для кобмбобокса команд
            SelectedGenre = genre;
            SelectedArtist = artist;
        }
        public SelectList Genres { get; } // список клубов
        public int SelectedGenre { get; } // выбранный клуб
        public string SelectedArtist { get; } // введенная позиция //для текстового поля
    }
}
