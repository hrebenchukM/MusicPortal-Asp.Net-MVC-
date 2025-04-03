using System.Collections.Generic;
using System;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;

namespace MusicPortal_Asp.Net_MVC_.DAL.Interfaces
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<int?> GetUserIdByRole(string role);

        Task<int?> GetUserIdByLogin(string login);
        Task<IQueryable<Song>> GetIQueryable();
    }
}
