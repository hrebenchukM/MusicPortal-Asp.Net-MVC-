using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.Models;

namespace MusicPortal_Asp.Net_MVC_.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MusicPortalContext _context;

        public GenreRepository(MusicPortalContext context)
        {
            _context = context;
        }
        public async Task<List<Genre>> GetList()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<Genre> Get(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task Create(Genre u)
        {
            await _context.Genres.AddAsync(u);
        }

        public void Update(Genre u)
        {
            _context.Genres.Update(u);
        }

        public async Task Delete(int id)
        {
            Genre? u = await _context.Genres.FindAsync(id);
            if (u != null)
                _context.Genres.Remove(u);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

   
        public async Task<bool> Exists(int id)
        {
            return await _context.Genres.AnyAsync(u => u.Id == id);
        }

    }
}