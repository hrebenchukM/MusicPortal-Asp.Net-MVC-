using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.DAL.Entities
{  
    public class User
    {

        public int Id { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Song>? Songs { get; set; }
    }
}
