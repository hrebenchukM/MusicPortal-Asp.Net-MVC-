using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.Models;

namespace MusicPortal_Asp.Net_MVC_.Repository
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<int?> GetUserIdByRole(string role);

        Task<int?> GetUserIdByLogin(string login);
        IQueryable<Song> GetIQueryable();


    }
}
