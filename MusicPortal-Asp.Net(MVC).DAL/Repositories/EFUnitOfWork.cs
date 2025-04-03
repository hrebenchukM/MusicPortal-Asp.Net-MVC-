using MusicPortal_Asp.Net_MVC_.DAL.EF;
using MusicPortal_Asp.Net_MVC_.DAL.Interfaces;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;

namespace MusicPortal_Asp.Net_MVC_.DAL.Repositories
{
    /*
     * Паттерн Unit of Work позволяет упростить работу с различными репозиториями и дает уверенность, 
     * что все репозитории будут использовать один и тот же контекст данных.
    */

    public class EFUnitOfWork : IUnitOfWork
    {
        private MusicPortalContext db;
        private SongRepository songRepository;
        private ArtistRepository artistRepository;
        private GenreRepository genreRepository;
        private UserRepository userRepository;

        public EFUnitOfWork(MusicPortalContext context)
        {
            db = context;
        }

        public IRepository<Artist> Artists
        {
            get
            {
                if (artistRepository == null)
                    artistRepository = new ArtistRepository(db);
                return artistRepository;
            }
        }

        public IRepository<Genre> Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }
        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public ISongRepository Songs
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }

       
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
       
    }
}