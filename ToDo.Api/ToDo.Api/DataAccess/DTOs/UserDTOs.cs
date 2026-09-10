using System.ComponentModel.DataAnnotations;

namespace ToDo.Api.DataAccess.DTOs
{
    public class UserDTOs
    {
        public class UserDTO
        {
            public int Id { get; set; }
            public string Email { get; set; } = string.Empty;

        }
        
        public class RegisterDTO
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            [MinLength(6)]
            public string Password { get; set; } = string.Empty;
        }

        public class LoginDTO
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            public string Password { get; set; } = string.Empty;
        }

        public class RefreshTokenDTO
        {
            [Required]
            public string RefreshToken { get; set; } = string.Empty;
        }

        public class TokenResponseDTO
        {
            public string AccessToken { get; set; } = string.Empty;

            public string RefreshToken { get; set; } = string.Empty;
        }
    }
}
