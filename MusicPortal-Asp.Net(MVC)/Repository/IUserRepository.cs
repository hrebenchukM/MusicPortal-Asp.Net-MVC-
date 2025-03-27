using MusicPortal_Asp.Net_MVC_.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal_Asp.Net_MVC_.Repository
{
    public interface IUserRepository:IRepository<User>
    {
        Task<bool> AnyUsers();


        Task<User?> GetUserByLogin(string login);

        Task ChangeActiveStatus(int id);
        Task<List<User>> GetInactiveUsers();

      
    }
}
