using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Services;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();//для интерфейса

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())//чтоб появился сам интерфейс, при словии если у нас приложение в состоянии разработки (не релиза и не тестирования)
//{// "ASPNETCORE_ENVIRONMENT": "Development"
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


app.UseHttpsRedirection();//перенаправление чтоб запросы шли по протоколу https 

app.UseAuthorization();

app.MapControllers();//добавляет таблиицу маршрутизации (маршруты с помощью атрибутов)


app.Run();

