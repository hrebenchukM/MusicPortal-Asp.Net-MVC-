using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.Models
{ 
    public class Artist
    {
        public Artist()
        {
            this.Songs = new HashSet<Song>();
        }
        public int Id { get; set; }

        [Display(Name = "Artist")]
        public string Name { get; set; }
        public ICollection<Song>? Songs { get; set; }
    }
}
