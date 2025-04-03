using System.Collections.Generic;
using System;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;

namespace MusicPortal_Asp.Net_MVC_.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> AnyUsers();


        Task<User?> GetUserByLogin(string login);

        Task ChangeActiveStatus(int id);
        Task<List<User>> GetInactiveUsers();

    }
}
