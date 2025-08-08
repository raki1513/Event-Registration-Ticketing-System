using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Application.IServices;
using EventTicketingSystem.Infrastructure.Repositories.IRepositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventTicketingSystem.Application.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IAuthentication _authRepo;
        private readonly JwtSettings _jwtSettings;
        public AuthServices(IAuthentication authRepo, IOptions<JwtSettings> jwtOptions)
        {
            _authRepo = authRepo;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<string?> LoginUser(LoginDTO loginDTO)
        {
            var user = await _authRepo.FindUserExist(loginDTO.userName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.password, user.PasswordHash)) {
                return null;
            }
            var roles = await _authRepo.GetUserRolesAsync(user.Id);
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:_jwtSettings.Issuer,
                audience:_jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
            throw new NotImplementedException();
        }
    }
}
