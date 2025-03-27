using MusicPortal_Asp.Net_MVC_.Models;

namespace MusicPortal_Asp.Net_MVC_.Repository
{
    public interface IRepository<T>
    {
        Task<List<T>> GetList();
        Task<T> Get(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
        Task Save();
        Task<bool> Exists(int id);
    }
}
