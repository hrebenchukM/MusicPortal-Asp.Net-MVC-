
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Services;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(180);
    options.Cookie.Name = "Session";

});
// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IPassword, PasswordService>();

builder.Services.AddSoccerContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IArtistService, ArtistService>();//нет проблем ибо это верхний и средний слой мы взяли добавили ссылку Add project refernce
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISongService, SongService>();



// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();


var app = builder.Build();
app.UseSession();

app.UseStaticFiles(); // обрабатывает запросы к файлам в папке wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
