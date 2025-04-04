using MusicPortal_Asp.Net_MVC_.BLL.DTO;

namespace MusicPortal_Asp.Net_MVC_.BLL.Interfaces
{
    public interface IPassword
    {
        string GenerateSalt();
        string HashPassword(string salt, string password);
    }
}
