using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.Models;

namespace MusicPortal_Asp.Net_MVC_.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly MusicPortalContext _context;

        public ArtistRepository(MusicPortalContext context)
        {
            _context = context;
        }
        public async Task<List<Artist>> GetList()
        {
            return await _context.Artists.ToListAsync();
        }
        public async Task<Artist> Get(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task Create(Artist u)
        {
            await _context.Artists.AddAsync(u);
        }

        public void Update(Artist u)
        {
            _context.Artists.Update(u);
        }

        public async Task Delete(int id)
        {
            Artist? u = await _context.Artists.FindAsync(id);
            if (u != null)
                _context.Artists.Remove(u);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<bool> Exists(int id)
        {
            return await _context.Artists.AnyAsync(u => u.Id == id);
        }

    }
}