
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
// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IPassword, PasswordService>();

builder.Services.AddSoccerContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IArtistService, ArtistService>();//��� ������� ��� ��� ������� � ������� ���� �� ����� �������� ������ Add project refernce
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISongService, SongService>();



// ��������� ������� MVC
builder.Services.AddControllersWithViews();


var app = builder.Build();
app.UseSession();

app.UseStaticFiles(); // ������������ ������� � ������ � ����� wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
