using Company.BLL.Services.Interfaces.IAuth;
using Company.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthManager _authManager) : ControllerBase
    {
        //Resiter
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            var userDto = await _authManager.RegisterAsync(registerDto);
            return Ok(userDto);
        }

        //Login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var userDto = await _authManager.LoginAsync(loginDto);
            return Ok(userDto);
        }

    }
}
