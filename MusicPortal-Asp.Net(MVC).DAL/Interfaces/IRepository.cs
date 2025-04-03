using System.Collections.Generic;
using System;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;

namespace MusicPortal_Asp.Net_MVC_.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetList();
        Task<T> Get(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
        Task<bool> Exists(int id);
    }

}
