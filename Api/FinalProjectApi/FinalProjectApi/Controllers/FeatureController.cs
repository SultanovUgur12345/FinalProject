using FinalProjectApi.DTOs.Feature;
using FinalProjectApi.Helpers.Enums;
using FinalProjectApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _featureService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _featureService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FeatureCreateDto dto)
        {
            await _featureService.CreateAsync(dto);
            return Ok();
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] FeatureUpdateDto dto)
        {
            await _featureService.UpdateAsync(id, dto);
            return Ok("Updated successfully");
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _featureService.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}
