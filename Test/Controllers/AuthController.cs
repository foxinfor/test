using BLL.DTO.Requests;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {   
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO loginDto, CancellationToken cancellationToken = default)
        {
            var result = await _authService.AuthAsync(loginDto, cancellationToken);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO userDto, CancellationToken cancellationToken = default)
        {
            var result = await _authService.RegisterAsync(userDto, cancellationToken);
            return Ok(result);
        }

        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _authService.RefreshTokenAsync(request, cancellationToken);
            return Ok(result);
        }

        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken = default)
        {
            await _authService.LogoutAsync(User, cancellationToken);
            return Ok();
        }
    }
}
