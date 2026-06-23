using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopLibrary.Contexts;
using ShopLibrary.DTOs;
using ShopLibrary.Models;
using ShopLibrary.Options;

namespace ShopLibrary.Services
{
    public class AuthorizationService(ProjectDbContext context, IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        private readonly ProjectDbContext _context = context;

        private bool VerifyPassword(string password, string passwordHash)
            => BCrypt.Net.BCrypt.Verify(password, passwordHash);

        public async Task<string?> AuthorizationUserWithTokenAsync(AuthorizationRequest request)
        {
            string login = request.Login;
            string password = request.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var user = await GetUserByLoginAsync(login);
            if (user == null)
                return null;

            if (VerifyPassword(password, user.Password))
                return await GenerateToken(user);
            return null;
        }

        private async Task<string?> GenerateToken(User user)
        {
            int id = user.UserId;
            string login = user.Login;

            var secretKey = _configuration.GetSection("JWT")["SecretKey"];

            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException("Нет секретного ключа!");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRole = await GetUserRoleByLoginAsync(login);

            if (userRole == null)
                return null;

            var claims = new Claim[]
            {
                new (ClaimTypes.NameIdentifier, id.ToString()),
                new (ClaimTypes.Name, login),
                new (ClaimTypes.Role, userRole.Name),
            };

            var token = new JwtSecurityToken(
                issuer: AuthorizationOptions.issuer,
                audience: AuthorizationOptions.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(AuthorizationOptions.lifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> GetUserByLoginAsync(string login)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Login == login) ?? null!;
        }

        public async Task<Role?> GetUserRoleByLoginAsync(string login)
        {
            var user = await _context.Users
                .Include(c => c.Role)
                .FirstOrDefaultAsync(cu => cu.Login == login);

            if (user == null)
                return null;
            return user.Role;
        }
    }
}
