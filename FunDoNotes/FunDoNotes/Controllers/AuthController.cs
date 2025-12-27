using FunDoNotes.BusinessLogic.Interfaces;
using FunDoNotes.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunDoNotes.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUserDto dto)
        {
            _userService.Register(dto);
            return Ok("User registered successfully");
        }

        [Authorize]
        [HttpGet("secure-test")]
        public IActionResult SecureTest()
        {
            return Ok("JWT is valid");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto dto)
        {
            var token = _userService.Login(dto);
            return Ok(new { token });
        }

    }
}
