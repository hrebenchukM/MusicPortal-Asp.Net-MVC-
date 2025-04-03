using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.DAL.Entities
{ 
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Song>? Songs { get; set; }
    }
}
