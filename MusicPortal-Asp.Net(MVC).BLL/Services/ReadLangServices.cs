using MusicPortal_Asp.Net_MVC_.BLL.DTO;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using MusicPortal_Asp.Net_MVC_.DAL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace MusicPortal_Asp.Net_MVC_.BLL.Services
{
    public class ReadLangServices : ILangRead
    {
        IConfiguration _con;
        List<Language> languageLists;
        public ReadLangServices(IConfiguration con)
        {
            string section = "Lang";//из appsettings
            _con = con;
            IConfigurationSection pointSection = _con.GetSection(section);
            List<Language> lists = new List<Language>();
            foreach (var language in pointSection.AsEnumerable())
            {
                if (language.Value != null)
                    lists.Add(new Language
                    {
                        ShortName = language.Key.Replace(section + ":", ""),
                        Name = language.Value
                    });
            }

            languageLists = lists;
        }

        public List<Language> languageList() => languageLists;//получаем список
    }
}
