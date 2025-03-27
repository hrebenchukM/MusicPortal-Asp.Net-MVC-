using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.Models;

namespace MusicPortal_Asp.Net_MVC_.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicPortalContext _context;

        public SongRepository(MusicPortalContext context)
        {
            _context = context;
        }
      
        public async Task<List<Song>> GetList()
        {
            return await _context.Songs
                                    .Include(s => s.Artist)
                                    .Include(s => s.Genre)
                                    .Include(s => s.User)
                                    .ToListAsync();
        }
        public async Task<Song?> Get(int id)
        {

            return await _context.Songs.Include(s => s.Artist)
                                       .Include(s => s.Genre)
                                       .Include(s => s.User)
                                       .FirstOrDefaultAsync(m => m.Id == id);
        }


        public async Task Create(Song s)
        {
            await _context.Songs.AddAsync(s);
        }

        public void Update(Song s)
        {
            _context.Songs.Update(s);
        }

        public async Task Delete(int id)
        {
            Song? s = await _context.Songs.FindAsync(id);
            if (s != null)
                _context.Songs.Remove(s);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<bool> Exists(int id)
        {
            return await _context.Songs.AnyAsync(s => s.Id == id);
        }

        public async Task<int?> GetUserIdByRole(string role)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Role == role);
            return user?.Id;
        }
        public async Task<int?> GetUserIdByLogin(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            return user?.Id;
        }

    }
}
