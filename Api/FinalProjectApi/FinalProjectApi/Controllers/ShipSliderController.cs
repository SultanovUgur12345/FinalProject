using FinalProjectApi.DTOs.ShipSlider;
using FinalProjectApi.Helpers.Enums;
using FinalProjectApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShipSliderController : ControllerBase
    {
        private readonly IShipSliderService _shipSliderService;

        public ShipSliderController(IShipSliderService shipSliderService)
        {
            _shipSliderService = shipSliderService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _shipSliderService.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpGet]
        public async Task<IActionResult> GetAllAdmin()
        {
            var result = await _shipSliderService.GetAllAdminAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _shipSliderService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ShipSliderCreateDto dto)
        {
            await _shipSliderService.CreateAsync(dto);
            return Ok();
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ShipSliderUpdateDto dto)
        {
            await _shipSliderService.UpdateAsync(id, dto);
            return Ok("Updated successfully");
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _shipSliderService.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}
