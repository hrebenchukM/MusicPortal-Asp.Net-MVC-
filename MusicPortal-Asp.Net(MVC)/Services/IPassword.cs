using System.Security.Cryptography;
using System.Text;

namespace MusicPortal_Asp.Net_MVC_.Services
{
    public interface IPassword
    {
        string GenerateSalt();
        string HashPassword(string salt, string password);
    }
}
