using MusicPortal_Asp.Net_MVC_.BLL.DTO;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;

namespace MusicPortal_Asp.Net_MVC_.BLL.Interfaces
{
    public interface ILangRead
    {
        List<Language> languageList();
    }
}
