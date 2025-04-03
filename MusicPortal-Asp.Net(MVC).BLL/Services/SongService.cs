using MusicPortal_Asp.Net_MVC_.BLL.DTO;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using MusicPortal_Asp.Net_MVC_.DAL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using AutoMapper;

namespace MusicPortal_Asp.Net_MVC_.BLL.Services
{
    public class SongService : ISongService
    {
        IUnitOfWork Database { get; set; }

        public SongService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateSong(SongDTO songDto)
        {

        var song = new Song
            {
                Id = songDto.Id,
                Title = songDto.Title,
                Year = songDto.Year,
                PathS = songDto.PathS,
                PathV = songDto.PathV,
                PathP = songDto.PathP,
                ArtistId = (int)songDto.ArtistId,
                GenreId = (int)songDto.GenreId,
                UserId = (int)songDto.UserId
            };
            await Database.Songs.Create(song);
            await Database.Save();
        }

        public async Task UpdateSong(SongDTO songDto)
        {
            var song = new Song
            {
                Id = songDto.Id,
                Title = songDto.Title,
                Year = songDto.Year,
                PathS = songDto.PathS,
                PathV = songDto.PathV,
                PathP = songDto.PathP,
                ArtistId = (int)songDto.ArtistId,
                GenreId = (int)songDto.GenreId,
                UserId = (int)songDto.UserId
            };
            Database.Songs.Update(song);
            await Database.Save();
        }

        public async Task DeleteSong(int id)
        {
            await Database.Songs.Delete(id);
            await Database.Save();
        }

        public async Task<SongDTO> GetSong(int id)
        {
            var song = await Database.Songs.Get(id);
            if (song == null)
                throw new ValidationException("Wrong song!", "");
            return new SongDTO
            {
                Id = song.Id,
                Title = song.Title,
                Year = song.Year,
                PathS = song.PathS,
                PathV = song.PathV,
                PathP = song.PathP,
                ArtistId = song.ArtistId,
                Artist = song.Artist?.Name,
                GenreId = song.GenreId,
                Genre = song.Genre?.Name,
                UserId = song.UserId,
                User = song.User?.Login
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.

        public async Task<IEnumerable<SongDTO>> GetSongs()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>()
            .ForMember("Artist", opt => opt.MapFrom(c => c.Artist.Name)).ForMember("Genre", opt => opt.MapFrom(c => c.Genre.Name)).ForMember("User", opt => opt.MapFrom(c => c.User.Login)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetList());
        }

    }
}
