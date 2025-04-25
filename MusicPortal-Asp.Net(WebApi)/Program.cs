using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Services;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ��� ������ �������� ������ ������� IDistributedCache, � ASP.NET Core 
// ������������� ���������� ���������� IDistributedCache
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // ������������ ������ (����-��� ���������� ������)
    options.Cookie.Name = "Session"; // ������ ������ ����� ���� �������������, ������� ����������� � �����.

});



// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddScoped<IPassword, PasswordService>();
builder.Services.AddSoccerContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddScoped<IArtistService, ArtistService>();//��� ������� ��� ��� ������� � ������� ���� �� ����� �������� ������ Add project refernce
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<ILangRead, ReadLangServices>();

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();//��� ����������

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();   // ��������� middleware-��������� ��� ������ � ��������

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())//���� �������� ��� ���������, ��� ������ ���� � ��� ���������� � ��������� ���������� (�� ������ � �� ������������)
//{// "ASPNETCORE_ENVIRONMENT": "Development"
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


app.UseHttpsRedirection();//��������������� ���� ������� ��� �� ��������� https 

app.UseAuthorization();

app.MapControllers();//��������� �������� ������������� (�������� � ������� ���������)


app.Run();

