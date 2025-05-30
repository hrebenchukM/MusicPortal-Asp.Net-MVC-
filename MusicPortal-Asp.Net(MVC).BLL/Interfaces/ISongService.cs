﻿using AutoMapper;
using MusicPortal_Asp.Net_MVC_.BLL.DTO;

namespace MusicPortal_Asp.Net_MVC_.BLL.Interfaces
{
    public interface ISongService 
    {
        Task<SongDTO> CreateSong(SongDTO songDto);
        Task UpdateSong(SongDTO songDto);
        Task DeleteSong(int id);
        Task<SongDTO> GetSong(int id);
        Task<IEnumerable<SongDTO>> GetSongs();
        IQueryable<SongDTO> GetSongsIQueryable();

        Task<bool> ExistsSong(int id);
        Task<int?> GetUserIdByRole(string role);
        Task<int?> GetUserIdByLogin(string login);
    }
}

