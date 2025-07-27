
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            var result = await serviceManager.UserService.RegisterAsync(registerDto);
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            var result = await serviceManager.UserService.LoginAsync(loginDto);
            return Ok(result);
        }
    }
}
