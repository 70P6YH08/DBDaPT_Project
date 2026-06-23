using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopLibrary.Contexts;
using ShopLibrary.DTOs;
using ShopLibrary.Models;
using ShopLibrary.Options;

namespace ShopLibrary.Services
{
    public class AuthorizationService(ProjectDbContext context)
    {

        private readonly ProjectDbContext _context = context;

        private bool VerifyPassword(string password, string passwordHash)
            => BCrypt.Net.BCrypt.Verify(password, passwordHash);

        public async Task<User?> AuthorizationUserAsync(AuthorizationRequset request)
        {
            string login = request.Login;
            string password = request.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var user = await GetUserByLoginAsync(login);
            if (user is null)
                return null;

            if (!VerifyPassword(password, user.Password))
                return null;
            return user;
        }

        public async Task<string?> AuthorizationUserWithTokenAsync(AuthorizationRequset request)
        {
            string login = request.Login;
            string password = request.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var user = await GetUserByLoginAsync(login);
            if (user is null)
                return null;

            return VerifyPassword(password, user.Password) ? await GenerateToken(user) : null;
        }

        private async Task<string> GenerateToken(User user)
        {
            int id = user.UserId;
            string login = user.Login;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthorizationOptions.secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRole = await GetUserRoleByLoginAsync(login);

            var claims = new Claim[]
            {
                    new ("id", id.ToString()),
                    new ("login", login),
                    new ("role", userRole.Name),
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
