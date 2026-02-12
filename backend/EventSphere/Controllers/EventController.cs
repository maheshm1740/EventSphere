using BLEventSphere;
using DLEventSphere.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSphere.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly BlEventOrganizer _organizerService;
        private readonly BlEvent _eventService;

        public EventController(
            BlEventOrganizer organizerService,
            BlEvent eventService)
        {
            _organizerService = organizerService;
            _eventService = eventService;
        }

        // ===================== CREATE EVENT =====================
        [HttpPost("create")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> CreateEvent([FromForm] CreateEventDto dto)
        {
            string? imageUrl = null;

            if (dto.Image != null)
            {
                var allowedExt = new[] { ".jpg", ".jpeg", ".png" };
                var ext = Path.GetExtension(dto.Image.FileName).ToLowerInvariant();

                if (!allowedExt.Contains(ext))
                    return BadRequest("Only JPG, JPEG, PNG allowed.");

                if (dto.Image.Length > 2 * 1024 * 1024)
                    return BadRequest("Image size must be <= 2MB.");

                var fileName = $"{Guid.NewGuid()}{ext}";
                var folderPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "event-images"
                );

                Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                imageUrl = $"/event-images/{fileName}";
            }

            long OrganizerId = ClaimsPrincipalExtensions.GetUserId(User);

            var result = await _organizerService.RegisterEvent(dto, imageUrl, OrganizerId);
            return Ok(result);
        }

        // ===================== UPDATE EVENT =====================
        [HttpPut("update/{eventId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateEvent(
            long eventId,
            [FromBody] UpdateEvent updateEvent)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response =
                await _organizerService.UpdateEvent(eventId, updateEvent);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(response);
        }

        // ===================== ORGANIZER EVENTS =====================
        [HttpGet("organizer")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetOrganizerEvents()
        {
            long organizerId = ClaimsPrincipalExtensions.GetUserId(User);
            var response =
                await _organizerService.GetOrganizerEvents(organizerId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // ===================== EVENT BY ID =====================
        [HttpGet("{eventId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventById(long eventId)
        {
            var response =
                await _organizerService.GetEventById(eventId);

            if (!response.Success)
                return NotFound(new { message = response.Message });

            return Ok(response);
        }

        // ===================== DELETE EVENT =====================
        [HttpDelete("{eventId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> DeleteEvent(long eventId)
        {
            long organizerId = ClaimsPrincipalExtensions.GetUserId(User);

            var response =
                await _organizerService.DeleteEvent(eventId, organizerId);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(response);
        }


        // ===================== ALL EVENTS =====================
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEvents()
        {
            var response = await _eventService.GetAllEvents();

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(response);
        }

        // ===================== USER REGISTERED EVENTS =====================
        [HttpGet("user/registered")]
        public async Task<IActionResult> GetUserRegisteredEvents()
        {
            var userId = GetUserId();
            var response =
                await _eventService.GetUserRegisteredEvents(userId);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(response);
        }

        // ===================== PARTICIPATE =====================
        [HttpPost("participate/{eventId}")]
        [Authorize(Roles = "Attendee")]
        public async Task<IActionResult> RegisterEvent(long eventId)
        {
            var userId = GetUserId();
            var result =
                await _eventService.RegisterForEvent(userId, eventId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // ===================== FEEDBACK =====================
        [HttpPost("feedback")]
        [Authorize(Roles = "Attendee")]
        public async Task<IActionResult> AddFeedback(AddFeedbackDto dto)
        {
            var userId = GetUserId();

            var result = await _eventService.AddFeedback(
                userId,
                dto.EventId,
                dto.Rating,
                dto.Comment
            );

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // ===================== JWT USER ID =====================
        private long GetUserId()
        {
            return long.Parse(User.FindFirst("userId")!.Value);
        }
    }
}
