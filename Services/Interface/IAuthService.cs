using PetsMobile.Entities;
using PetsMobile.Services.DTO;

namespace PetsMobile.Services.Interface
{
    public interface IAuthService
    {
        record AuthResult(
            UserDTO User,
            string AccessToken,
            RefreshToken RefreshToken
        );
        Task<AuthResult?> LoginAsync(string email, string password);
        Task<AuthResult?> RefreshAsync(string refreshToken);
        Task<bool> LogoutAsync (string refreshToken);
    }
}
