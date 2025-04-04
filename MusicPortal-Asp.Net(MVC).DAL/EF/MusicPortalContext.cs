using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;

namespace MusicPortal_Asp.Net_MVC_.DAL.EF
{   
    public class MusicPortalContext : DbContext
    {
  

        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {

                string salt = "018035F9A34E797385FCF35335B9B18E";
                string password = "4EC7CD4C352522EF9BA1FE7BE384FAD853D4E59E412DEAA644581C8D21630B0C";


                User admin1 = new User { FirstName = "admin", LastName = "admin", Login = "admin1@gmail.com", Password = password, Salt = salt, Role = "Admin", IsActive = true };
                Users?.Add(admin1);






                Genre genre = new Genre { Name = "Pop" };
                Genres?.Add(genre);

                Genre genre2 = new Genre { Name = "Rep" };
                Genres?.Add(genre2);



                Genre genre3 = new Genre { Name = "Hip-Hop" };
                Genres?.Add(genre3);

                Genre genre4 = new Genre { Name = "Dance-Pop" };
                Genres?.Add(genre4);

                SaveChanges();
                Artist art1 = new Artist { Name = "SawanoHiroyuki[nZk]" };
                Artists?.Add(art1);


                Artist art2 = new Artist { Name = "Marcus King" };
                Artists?.Add(art2);

                Artist art3 = new Artist { Name = "Gibbs" };
                Artists?.Add(art3);


                Artist art4 = new Artist { Name = "Carla's Dreams" };
                Artists?.Add(art4);


                Artist art5 = new Artist { Name = "Artem Pivovarov " };
                Artists?.Add(art5);

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



                Song song4 = new Song
                {
                    Title = "Train to be continued",
                    Year = 2021,
                    ArtistId = 3,
                    GenreId = 2,
                    UserId = admin1.Id,
                    PathS = "/sounds/TwojeSerceProblemy.mp3",
                    PathV = "/video/TwojeSerceProblemy.mp4",
                    PathP = "/Images/TwojeSerceProblemy.jpg"
                };


                Song song5 = new Song
                {
                    Title = "Scara 2, etajul 7",
                    Year = 2020,
                    ArtistId = 4,
                    GenreId = 3,
                    UserId = admin1.Id,
                    PathS = "/sounds/Scara.mp3",
                    PathV = "/video/Scara.mp4",
                    PathP = "/Images/Scara.jpg"
                };

                Song song6 = new Song
                {
                    Title = "Deja Vu",
                    Year = 2019,
                    ArtistId = 5,
                    GenreId = 4,
                    UserId = admin1.Id,
                    PathS = "/sounds/DejaVu.mp3",
                    PathV = "/video/DejaVu.mp4",
                    PathP = "/Images/DejaVu.jpg"
                };


                Songs?.Add(song1);
                Songs?.Add(song2);
                Songs?.Add(song3);
                Songs?.Add(song4);
                Songs?.Add(song5);
                Songs?.Add(song6);
                SaveChanges();


                SaveChanges();
            }
        }


    }
}
