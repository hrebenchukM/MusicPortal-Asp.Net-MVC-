using MusicPortal_Asp.Net_MVC_.Models;
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.Repository;
using MusicPortal_Asp.Net_MVC_.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(180);
    options.Cookie.Name = "Session";

}); 

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IPassword, PasswordService>();

builder.Services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));


builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

var app = builder.Build();
app.UseSession();   
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
