using Microsoft.Extensions.DependencyInjection;
using MusicPortal_Asp.Net_MVC_.DAL.Interfaces;
using MusicPortal_Asp.Net_MVC_.DAL.Repositories;

namespace MusicPortal_Asp.Net_MVC_.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
