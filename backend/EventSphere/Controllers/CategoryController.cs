using BLEventSphere;
using DLEventSphere.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DLEventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class CategoryController : ControllerBase
    {
        private readonly BlCategory _categoryService;

        public CategoryController(BlCategory categoryService)
        {
            _categoryService = categoryService;
        }

        // ===================== CREATE CATEGORY =====================
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(
            [FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _categoryService.CreateCategory(dto.Name);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(new
            {
                success = true,
                message = response.Message,
                data = response.Data
            });
        }

        // ===================== GET ALL =====================
        [HttpGet]
        [AllowAnonymous] // public list (recommended)
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllCategories();

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(new
            {
                success = true,
                data = response.Data
            });
        }

        // ===================== GET BY ID =====================
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetCategoryById(id);

            if (!response.Success)
                return NotFound(new { message = response.Message });

            return Ok(new
            {
                success = true,
                data = response.Data
            });
        }

        // ===================== DELETE =====================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategory(id);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return Ok(new
            {
                success = true,
                message = response.Message
            });
        }
    }
}
