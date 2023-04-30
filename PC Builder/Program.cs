using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using PC_Builder.Models;    // ������������ ���� ������ ApplicationContext

var builder = WebApplication.CreateBuilder(args);

// �������������� � ������� ����
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie(options => options.LoginPath = "/Account/Login");
builder.Services.AddAuthorization();

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseAuthentication();   // ���������� middleware �������������� 
app.UseAuthorization();   // ���������� middleware ����������� 

app.MapDefaultControllerRoute();

app.UseStaticFiles(); // ��������� ������������ ����������� ������, �������� css

app.Run();
