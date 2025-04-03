using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using MusicPortal_Asp.Net_MVC_.DAL.Interfaces;
using MusicPortal_Asp.Net_MVC_.DAL.EF;


namespace MusicPortal_Asp.Net_MVC_.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MusicPortalContext db;

        public UserRepository(MusicPortalContext context)
        {
            this.db = context;
        }

        public async Task<List<User>> GetList()
        {
            return await db.Users.ToListAsync();
        }
        public async Task<User> Get(int id)
        {
            User? user = await db.Users.FindAsync(id);
            return user!;
        }


        public async Task<User> Get(string login)
        {
            var users = await db.Users.Where(a => a.Login == login).ToListAsync();
            User? user = users?.FirstOrDefault();
            return user!;
        }

        public async Task Create(User user)
        {
            await db.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            User? user = await db.Users.FindAsync(id);
            if (user != null)
                db.Users.Remove(user);
        }


        public async Task<bool> Exists(int id)
        {
            return await db.Users.AnyAsync(user => user.Id == id);
        }



        public async Task<bool> AnyUsers()
        {
            return await db.Users.AnyAsync();
        }


        public async Task<User?> GetUserByLogin(string login)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task ChangeActiveStatus(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
            }
        }

        public async Task<List<User>> GetInactiveUsers()
        {
            return await db.Users.Where(u => !u.IsActive).ToListAsync();
        }

    }
}
