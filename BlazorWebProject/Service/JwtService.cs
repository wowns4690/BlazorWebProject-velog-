using BlazorWebProject.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorWebProject.Service
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public async Task<string> GenerateJwtTokenAsync(string username)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: new[] {
                    new Claim(ClaimTypes.Name, username),
                },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
