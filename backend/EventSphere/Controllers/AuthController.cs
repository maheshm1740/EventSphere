using BLEventSphere;
using DLEventSphere.DTO_s;
using Microsoft.AspNetCore.Mvc;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BlUser _userService;
        private readonly JwtService _jwtService;

        public AuthController(BlUser userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // ===================== REGISTER =====================
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response =
                await _userService.AddUser(userRegistration, isAdmin: false);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(new
            {
                success = true,
                message = response.Message
            });
        }

        // ===================== LOGIN =====================
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] AuthDetails auth)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.AuthenticateUser(auth);

            if (!result.Success || result.Data == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = result.Message
                });
            }

            var token = _jwtService.GenerateToken(
                result.Data.UserId.ToString(),
                result.Data.Email,
                result.Data.Role.ToString()
            );

            return Ok(new
            {
                success = true,
                message = "Login successful!",
                token,
                user = new
                {
                    userId = result.Data.UserId,
                    email = result.Data.Email,
                    role = result.Data.Role.ToString()
                }
            });
        }
    }
}
