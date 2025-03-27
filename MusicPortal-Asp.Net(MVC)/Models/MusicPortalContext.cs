using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.Services;

namespace MusicPortal_Asp.Net_MVC_.Models
{
    public class MusicPortalContext : DbContext
    {
        private readonly IPassword _passwordService;


        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public MusicPortalContext(DbContextOptions<MusicPortalContext> options, IPassword ps)
           : base(options)
        {
            _passwordService = ps;
            if (Database.EnsureCreated())
            {

                string salt = _passwordService.GenerateSalt();
                string password = _passwordService.HashPassword(salt, "admin123");


                User admin1 = new User { FirstName = "admin", LastName = "admin", Login = "admin1@gmail.com",Password= password, Salt= salt, Role = "Admin",IsActive = true };
                Users?.Add(admin1);


                Genre genre = new Genre { Name = "Pop" };
                Genres?.Add(genre);
                SaveChanges();
                Artist art1 = new Artist { Name = "SawanoHiroyuki[nZk]" };
                Artists?.Add(art1);


                Artist art2 = new Artist { Name = "Marcus King" };
                Artists?.Add(art2);

              
                SaveChanges();
                Song song1 = new Song
                {
                    Title = "Dark Aria",
                    Year = 2024,
                    ArtistId = 1,  
                    GenreId = 1,   
                    UserId = admin1.Id,
                    PathS = "/sounds/DarkAria.mp3",
                    PathV = "/video/Aria.mp4",
                    PathP = "/Images/Aria.jpg"
                };


                Song song2 = new Song
                {
                    Title = "Sucker",
                    Year = 2024,
                    ArtistId = 2,
                    GenreId = 1,
                    UserId = admin1.Id,
                    PathS = "/sounds/Sucker.mp3",
                    PathV = "/video/Sucker.mp4",
                    PathP = "/Images/Sucker.jpg"
                };

                Song song3 = new Song
                {
                    Title = "Shadowborn ",
                    Year = 2024,
                    ArtistId = 1,
                    GenreId = 1,
                    UserId = admin1.Id,
                    PathS = "/sounds/Shadowborn.mp3",
                    PathV = "/video/Shadowborn.mp4",
                    PathP = "/Images/Shadowborn.jpg"
                };

                Songs?.Add(song1);
                Songs?.Add(song2);
                Songs?.Add(song3);
                SaveChanges();


                SaveChanges();
            }
        }

       
    }
}