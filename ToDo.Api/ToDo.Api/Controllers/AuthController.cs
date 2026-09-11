using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Api.Interfaces.AuthInterfaces;
using static ToDo.Api.DataAccess.DTOs.UserDTOs;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterDTO dto)
        {
            var user = await _authService.RegisterAsync(dto);
            if (user == null)
            {
                return BadRequest("User with this email already exists.");
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDTO>> Login([FromBody] LoginDTO dto)
        {
            var tokens = await _authService.LoginAsync(dto);
            if (tokens == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(tokens);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<TokenResponseDTO>> Refresh([FromBody] RefreshTokenDTO dto)
        {
            var tokens = await _authService.RefreshTokenAsync(dto);
            if (tokens == null)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }
            return Ok(tokens);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }


            var result = await _authService.LogoutAsync(userId);
            if (!result)
            {
                return BadRequest("Logout failed.");
            }

            return Ok();
        }
    }
}
