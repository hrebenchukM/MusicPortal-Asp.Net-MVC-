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

        public async Task <SongDTO> CreateSong(SongDTO songDto)
        {

        var song = new Song
            {
                Title = songDto.Title,
                Year = songDto.Year,
                PathS = songDto.PathS,
                PathV = songDto.PathV,
                PathP = songDto.PathP,
                ArtistId = songDto.ArtistId.HasValue ? songDto.ArtistId.Value : 0, 
                GenreId = songDto.GenreId.HasValue ? songDto.GenreId.Value : 0, 
                UserId = songDto.UserId.HasValue ? songDto.UserId.Value : 0
             };
            await Database.Songs.Create(song);
            await Database.Save();

            return new SongDTO
            {
                Id = song.Id,
                Title = song.Title,
                Year = song.Year,
                PathS = song.PathS,
                PathV = song.PathV,
                PathP = song.PathP,
                ArtistId = (int)song.ArtistId,
                GenreId = (int)song.GenreId,
                UserId = (int)song.UserId
            };
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
        //Метод возвращает IEnumerable<SongDTO>. Это коллекция объектов DTO, полученных после выполнения запроса к базе данных.
        public async Task<IEnumerable<SongDTO>> GetSongs()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>()
            .ForMember("Artist", opt => opt.MapFrom(c => c.Artist.Name)).ForMember("Genre", opt => opt.MapFrom(c => c.Genre.Name)).ForMember("User", opt => opt.MapFrom(c => c.User.Login)));
            var mapper = new Mapper(config);
            //Запрос к базе данных выполняется асинхронно и возвращает список объектов типа Song
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetList());//Маппинг выполняется в памяти, а не в запросе к базе данных, поэтому все элементы коллекции сначала загружаются в память
        }
        //Метод возвращает IQueryable<SongDTO>. Это запрос, который можно дополнительно изменять и исполнять на базе данных.
        public IQueryable<SongDTO> GetSongsIQueryable()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>()
             .ForMember("Artist", opt => opt.MapFrom(c => c.Artist.Name)).ForMember("Genre", opt => opt.MapFrom(c => c.Genre.Name)).ForMember("User", opt => opt.MapFrom(c => c.User.Login)));
            var mapper = config.CreateMapper();
            //Позволяет работать с данными в виде запроса, а не с уже загруженными объектами
            return mapper.ProjectTo<SongDTO>(Database.Songs.GetIQueryable());//ProjectTo<SongDTO> применяется непосредственно к IQueryable, что означает, что преобразование объектов Song в SongDTO происходит на уровне базы данных.
        }





        public async Task<bool> ExistsSong(int id)
        {
            return await Database.Songs.Exists(id);
        }


        public async Task<int?> GetUserIdByRole(string role)
        {
            return await Database.Songs.GetUserIdByRole(role);
        }

        public async Task<int?> GetUserIdByLogin(string login)
        {
            return await Database.Songs.GetUserIdByLogin(login);
        }
    }
}
