using BLL.DTO.Requests;
using BLL.DTO.Responces;
using System.Security.Claims;

namespace BLL.Interfaces
{

    public interface IAuthService
    {
        Task<AuthResponseDTO> AuthAsync(LoginDTO loginDto, CancellationToken cancellationToken = default);
        Task<RegistrationResponseDTO> RegisterAsync(RegisterDTO registerDto, CancellationToken cancellationToken = default);
        Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
        Task LogoutAsync(ClaimsPrincipal principal, CancellationToken cancellationToken = default);
    }
}
