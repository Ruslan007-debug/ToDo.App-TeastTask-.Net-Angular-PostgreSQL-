namespace ToDo.Api.DataAccess.DTOs
{
    public class UserDTOs
    {
        public class UserDTO
        {
            public int Id { get; set; }
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

        }
    }
}
