using ExampeCrud.DTOs.Users;
using ExampeCrud.Models;
using ExampeCrud.Services.Interfaces;
using ExampeCrud.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampeCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserServices userServices) : ControllerBase
    {
        private readonly IUserServices _userService = userServices;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ApiResponseHelper.Failed<List<Users>>("data not found"));
            }

            var result = await _userService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _userService.LoginAsync(request);
            return Ok(result);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
           var result = await _userService.GetProfileAsync();
           return Ok(result);
        }
    }
}