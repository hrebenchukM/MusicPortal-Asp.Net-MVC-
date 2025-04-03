using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using MusicPortal_Asp.Net_MVC_.DAL.Interfaces;
using MusicPortal_Asp.Net_MVC_.DAL.EF;

namespace MusicPortal_Asp.Net_MVC_.DAL.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        private MusicPortalContext db;

        public GenreRepository(MusicPortalContext context)
        {
            this.db = context;
        }

        public async Task<List<Genre>> GetList()
        {
            return await db.Genres.ToListAsync();
        }
        public async Task<Genre> Get(int id)
        {
            Genre? genre = await db.Genres.FindAsync(id);
            return genre!;
        }


        public async Task<Genre> Get(string name)
        {
            var genres = await db.Genres.Where(a => a.Name == name).ToListAsync();
            Genre? genre = genres?.FirstOrDefault();
            return genre!;
        }

        public async Task Create(Genre genre)
        {
            await db.Genres.AddAsync(genre);
        }

        public void Update(Genre genre)
        {
            db.Entry(genre).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Genre? genre = await db.Genres.FindAsync(id);
            if (genre != null)
                db.Genres.Remove(genre);
        }


        public async Task<bool> Exists(int id)
        {
            return await db.Genres.AnyAsync(genre => genre.Id == id);
        }




    }
}
