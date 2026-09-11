using static ToDo.Api.DataAccess.DTOs.UserDTOs;

namespace ToDo.Api.Interfaces.AuthInterfaces
{
    public interface IAuthService
    {
        public Task<UserDTO?> RegisterAsync(RegisterDTO dto);
        public Task<TokenResponseDTO?> LoginAsync(LoginDTO loginDTO);
        public Task<TokenResponseDTO?> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO);
        public Task<bool> LogoutAsync(int userId);
    }
}
