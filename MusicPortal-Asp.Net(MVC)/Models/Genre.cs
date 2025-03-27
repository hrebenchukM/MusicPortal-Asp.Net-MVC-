using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Songs = new HashSet<Song>();
        }
        public int Id { get; set; }

        [Display(Name = "Genre")]
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
