using BLEventSphere;
using DLEventSphere.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // default auth
    public class UserController : ControllerBase
    {
        private readonly BlUser _userService;

        public UserController(BlUser userService)
        {
            _userService = userService;
        }

        // ===================== CHANGE PASSWORD =====================
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePassword changePassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();

            var response =
                await _userService.ChangePasswordAsync(userId, changePassword);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(new
            {
                success = true,
                message = response.Message
            });
        }

        // ===================== ADD ORGANIZER =====================
        [HttpPost("add-organizer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrganizer(
            [FromBody] UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response =
                await _userService.AddUser(userRegistration, isAdmin: true);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(new
            {
                success = true,
                message = response.Message
            });
        }

        // ===================== GET ALL USERS =====================
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(new
            {
                success = true,
                data = response.Data
            });
        }

        // ===================== JWT USER ID =====================
        private long GetUserId()
        {
            return long.Parse(User.FindFirst("userId")!.Value);
        }
    }
}
