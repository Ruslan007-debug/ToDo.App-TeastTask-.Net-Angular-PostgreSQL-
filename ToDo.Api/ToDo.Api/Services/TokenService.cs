using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDo.Api.DataAccess.Models;
using ToDo.Api.Interfaces.AuthInterfaces;

namespace ToDo.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken(User user)
        {
            var claims = new List<Claim>        //записуємо в токен айді та пошту
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)); //із секрету створюємо ключ для підпису токена

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //підписуємо токен за допомогою ключа та алгоритму HMAC SHA256

            var expires = int.Parse(_configuration["Jwt:AccessTokenMinutes"]!); //отримуємо час життя токена з конфігурації

            var token = new JwtSecurityToken(       //створюємо токен
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expires),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);     //повертаємо токен у вигляді рядка
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())    //створюємо випадковий масив байтів для refresh токена криптографічно безпечним способом
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);    //повертаємо refresh токен у вигляді рядка
            }
        }
    }
}
