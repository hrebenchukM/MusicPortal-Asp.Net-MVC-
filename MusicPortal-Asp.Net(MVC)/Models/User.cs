using System.ComponentModel.DataAnnotations;

namespace MusicPortal_Asp.Net_MVC_.Models
{
    public class User
    {
        public User()
        {
            this.Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Salt")]
        public string Salt { get; set; }

        [Display(Name = "Access role")]
        public string Role { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Songs")]
        public ICollection<Song>? Songs { get; set; }
    }
}
