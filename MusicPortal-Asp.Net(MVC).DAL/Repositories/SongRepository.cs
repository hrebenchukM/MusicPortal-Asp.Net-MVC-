using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using MusicPortal_Asp.Net_MVC_.DAL.Interfaces;
using MusicPortal_Asp.Net_MVC_.DAL.EF;


namespace MusicPortal_Asp.Net_MVC_.DAL.Repositories
{
    public class SongRepository : ISongRepository
    {
        private MusicPortalContext db;

        public SongRepository(MusicPortalContext context)
        {
            this.db = context;
        }

        public async Task<List<Song>> GetList()
        {
               return await db.Songs.Include(s => s.Artist)
                                    .Include(s => s.Genre)
                                    .Include(s => s.User)
                                    .ToListAsync();
        }
        public IQueryable<Song> GetIQueryable()
        {
            return db.Songs
                     .Include(s => s.Artist)
                     .Include(s => s.Genre)
                     .Include(s => s.User);
        }

        public async Task<Song> Get(int id)
        {
            var songs = await db.Songs.Include(s => s.Artist)
                                       .Include(s => s.Genre)
                                       .Include(s => s.User).Where(a => a.Id == id).ToListAsync();
            Song? song = songs?.FirstOrDefault();
            return song!;
        }


        public async Task<Song> Get(string name)
        {         
            var songs = await db.Songs.Where(a => a.Title == name).ToListAsync();
            Song? song = songs?.FirstOrDefault();
            return song!;
        }


        public async Task<int?> GetUserIdByRole(string role)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Role == role);
            return user?.Id;
        }
        public async Task<int?> GetUserIdByLogin(string login)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Login == login);
            return user?.Id;
        }


        public async Task Create(Song song)
        {
            await db.Songs.AddAsync(song);
        }

        public void Update(Song song)
        {
            db.Entry(song).State = EntityState.Modified;
        }


        public async Task Delete(int id)
        {
            Song? song = await db.Songs.FindAsync(id);
            if (song != null)
                db.Songs.Remove(song);
        }

        public async Task<bool> Exists(int id)
        {
            return await db.Songs.AnyAsync(s => s.Id == id);
        }
    

    }
}
