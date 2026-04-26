using FinalProjectApi.DTOs.Partner;
using FinalProjectApi.Helpers.Enums;
using FinalProjectApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _partnerService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _partnerService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PartnerCreateDto dto)
        {
            await _partnerService.CreateAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] PartnerUpdateDto dto)
        {
            await _partnerService.UpdateAsync(id, dto);
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _partnerService.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}
