using MusicPortal_Asp.Net_MVC_.BLL.DTO;

namespace MusicPortal_Asp.Net_MVC_.BLL.Interfaces
{
    public interface IArtistService 
    {
        Task<ArtistDTO> CreateArtist(ArtistDTO artistDto);
        Task UpdateArtist(ArtistDTO artistDto);
        Task DeleteArtist(int id);
        Task<ArtistDTO> GetArtist(int id);
        Task<IEnumerable<ArtistDTO>> GetArtists();
        Task<bool> ExistsArtist(int id);
    }
}
