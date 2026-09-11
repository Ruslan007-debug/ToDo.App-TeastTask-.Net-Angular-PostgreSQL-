using ToDo.Api.DataAccess.Models;

namespace ToDo.Api.Interfaces.AuthInterfaces
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);
        string CreateRefreshToken();
    }
}
