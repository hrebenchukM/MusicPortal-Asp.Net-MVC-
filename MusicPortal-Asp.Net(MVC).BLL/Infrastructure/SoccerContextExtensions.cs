using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.DAL.EF;


namespace MusicPortal_Asp.Net_MVC_.BLL.Infrastructure
{
    public static class SoccerContextExtensions
    {
        public static void AddSoccerContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));
        }
    }
}
