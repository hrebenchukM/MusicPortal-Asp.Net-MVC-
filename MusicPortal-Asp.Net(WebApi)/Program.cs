using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Services;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;

// https://developer.mozilla.org/ru/docs/Web/HTTP/CORS

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(); // ��������� ������� CORS



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

builder.Services.AddSwaggerGen();//��� ����������

var app = builder.Build();
// ����� ������������� CORS ��� ��������� ������� ���������� ����� app.UseCors()
// ��� ������������ ���������� CORS ���� ����� ���������� �������,
// � ������� ���������� ������ CorsPolicyBuilder.
//app.UseCors(builder => builder.AllowAnyOrigin());
// AllowAnyOrigin() ����������� ������� � ����� �������/�������.
// AllowAnyHeader() ����������� ������� � ������ �����������.
// AllowAnyMethod() ����������� ������� ������ ����. 
// AllowCredentials() ����������� ��������� ����������������� ������ �� ������� (��������, ����).
// WithHeaders() ����������� ������ �� �������, ������� �������� ������������ ���������.
// WithMethods() ����������� ������� ������ ������������� ����.
// WithOrigins() ����������� ������� ������ � ������������ �������.
// WithExposedHeaders() ��������� ������� ���������� �� ������� ������� ���� ���������.
// https://developer.mozilla.org/ru/docs/Web/HTTP/Headers

//��������� ���� ��� ����������� ������� �������������� �� ���� ������, ������ ��� �������� �������� ����� ����������� �������� ������������, �� ���� �� ����� �������� ������������ �������
//����� ���������-����� �� ������ ������ - �� ��� ������������ � ��������� ���� ������(�������� ���� , ����������,��������)
//����� ������ -���,����,���,�����
app.UseCors(builder => builder.WithOrigins("https://localhost:7245")
                    .AllowAnyHeader().AllowAnyMethod());





app.UseSession();   // ��������� middleware-��������� ��� ������ � ��������

//// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())//���� �������� ��� ���������, ��� ������ ���� � ��� ���������� � ��������� ���������� (�� ������ � �� ������������)
{// "ASPNETCORE_ENVIRONMENT": "Development"
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();//��������������� ���� ������� ��� �� ��������� https 

app.UseAuthorization();

app.MapControllers();//��������� �������� ������������� (�������� � ������� ���������)


app.Run();

