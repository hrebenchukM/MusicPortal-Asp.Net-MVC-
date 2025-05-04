using MusicPortal_Asp.Net_MVC_;
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Services;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// Для использования функциональности библиотеки SignalR,
// в приложении необходимо зарегистрировать соответствующие сервисы
builder.Services.AddSignalR();



// Все сессии работают поверх объекта IDistributedCache, и ASP.NET Core 
// предоставляет встроенную реализацию IDistributedCache
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Длительность сеанса (тайм-аут завершения сеанса)
    options.Cookie.Name = "Session"; // Каждая сессия имеет свой идентификатор, который сохраняется в куках.

});
// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IPassword, PasswordService>();

builder.Services.AddSoccerContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddScoped<IArtistService, ArtistService>();//нет проблем ибо это верхний и средний слой мы взяли добавили ссылку Add project refernce
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISongService, SongService>();

builder.Services.AddScoped<ILangRead, ReadLangServices>();


// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();


var app = builder.Build();
app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

app.UseStaticFiles(); // обрабатывает запросы к файлам в папке wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificationHub>("/notification");   // NotificationHub будет обрабатывать запросы по пути /notification


app.Run();
