using Microsoft.AspNetCore.Identity;
using ToDo.Api.DataAccess.DTOs;
using ToDo.Api.DataAccess.Models;
using ToDo.Api.Interfaces;
using ToDo.Api.Interfaces.AuthInterfaces;
using static ToDo.Api.DataAccess.DTOs.UserDTOs;

namespace ToDo.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository userRepository, ITokenService tokenService,
            IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }
        public async Task<TokenResponseDTO?> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return null;
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var accessToken = _tokenService.CreateAccessToken(user);
            var refreshToken = _tokenService.CreateRefreshToken();
            var refreshDays = int.Parse(_configuration["Jwt:RefreshTokenDays"]!);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshDays);
            await _userRepository.UpdateAsync(user, user.Id);
            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userRepository.UpdateAsync(user, user.Id);
            return true;
        }

        public async Task<TokenResponseDTO?> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshTokenDTO.RefreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow || user.RefreshTokenExpiryTime == null)
            {
                return null;
            }
            var newAccessToken = _tokenService.CreateAccessToken(user);
            var newRefreshToken = _tokenService.CreateRefreshToken();
            var refreshDays = int.Parse(_configuration["Jwt:RefreshTokenDays"]!);

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshDays);
            await _userRepository.UpdateAsync(user, user.Id);

            return new TokenResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<UserDTO?> RegisterAsync(RegisterDTO dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return null; 
            }
            var user = new User
            {
                Email = dto.Email,  
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            var createdUser = await _userRepository.CreateAsync(user);
            return new UserDTO
            {
                Id = createdUser.Id,
                Email = createdUser.Email
            };

        }
    }
}
