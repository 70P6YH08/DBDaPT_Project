using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using ShopLibrary.Contexts;
using ShopLibrary.Options;
using ShopLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var secretKey = builder.Configuration.GetSection("JWT")["SecretKey"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

        ValidateIssuer = true,
        ValidIssuer = AuthorizationOptions.issuer,
        ValidateAudience = true,
        ValidAudience = AuthorizationOptions.audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1),
    };
});


builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddDbContext<ProjectDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
