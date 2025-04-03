using MusicPortal_Asp.Net_MVC_.DAL.Entities;

namespace MusicPortal_Asp.Net_MVC_.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Artist> Artists { get; }
        IRepository<Genre> Genres { get; }
        IUserRepository Users { get; }
        ISongRepository Songs { get; }
        Task Save();
    }
}
