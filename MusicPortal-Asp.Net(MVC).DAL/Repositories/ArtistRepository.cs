using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using MusicPortal_Asp.Net_MVC_.DAL.Interfaces;
using MusicPortal_Asp.Net_MVC_.DAL.EF;

namespace MusicPortal_Asp.Net_MVC_.DAL.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private MusicPortalContext db;

        public ArtistRepository(MusicPortalContext context)
        {
            this.db = context;
        }

        public async Task<List<Artist>> GetList()
        {
            return await db.Artists.ToListAsync();
        }

        public async Task<Artist> Get(int id)
        {
            Artist? artist = await db.Artists.FindAsync(id);
            return artist!;
        }


        public async Task<Artist> Get(string name)
        {
            var artists = await db.Artists.Where(a => a.Name == name).ToListAsync();
            Artist? artist = artists?.FirstOrDefault();
            return artist!;
        }



        public async Task Create(Artist artist)
        {
            await db.Artists.AddAsync(artist);
        }

        public void Update(Artist artist)
        {
            db.Entry(artist).State = EntityState.Modified;
        }


        public async Task Delete(int id)
        {
            Artist? artist = await db.Artists.FindAsync(id);
            if (artist != null)
                db.Artists.Remove(artist);
        }


        public async Task<bool> Exists(int id)
        {
            return await db.Artists.AnyAsync(artist => artist.Id == id);
        }

       
    }
}
