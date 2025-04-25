using MusicPortal_Asp.Net_MVC_.BLL.DTO;

namespace MusicPortal_Asp.Net_MVC_.BLL.Interfaces
{
    public interface IGenreService 
    {
        Task<GenreDTO> CreateGenre(GenreDTO genreDto);
        Task UpdateGenre(GenreDTO genreDto);
        Task DeleteGenre(int id);
        Task<GenreDTO> GetGenre(int id);
        Task<IEnumerable<GenreDTO>> GetGenres();

        Task<bool> ExistsGenre(int id);
    }
}
