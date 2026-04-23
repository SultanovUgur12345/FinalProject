using FinalProjectApi.DTOs.AboutUs;
using FinalProjectApi.Helpers.Enums;
using FinalProjectApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AboutUsController : ControllerBase
    {
        private readonly IAboutUsService _service;

        public AboutUsController(IAboutUsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpGet]
        public async Task<IActionResult> GetAllAdmin()
        {
            var result = await _service.GetAllAdminAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutUsCreateDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] AboutUsUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok("Updated successfully");
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}
