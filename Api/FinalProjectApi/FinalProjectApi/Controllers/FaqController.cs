using FinalProjectApi.DTOs.Faq;
using FinalProjectApi.Helpers.Enums;
using FinalProjectApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _faqService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _faqService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FaqCreateDto dto)
        {
            await _faqService.CreateAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FaqUpdateDto dto)
        {
            await _faqService.UpdateAsync(id, dto);
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _faqService.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}
