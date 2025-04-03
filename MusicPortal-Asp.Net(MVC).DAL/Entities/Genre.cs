using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.DAL.Entities
{  
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
