using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ShopLibrary.Contexts;
using ShopLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

//builder.Services.AddDbContext<ProjectDbContext>(options =>
//    options.UseMySQL(
//        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ProjectDbContext>();
builder.Services.AddScoped<AuthorizationService>();

CultureInfo.DefaultThreadCurrentCulture =
    new("ru-RU") { NumberFormat = { NumberDecimalSeparator = "." } };

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();