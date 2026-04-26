using FinalProjectApi.DTOs.Worker;
using FinalProjectApi.Helpers.Enums;
using FinalProjectApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _workerService.GetAllAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var result = await _workerService.GetPagedAsync(page, pageSize);
            return Ok(result);
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var all = await _workerService.GetAllAsync();
                return Ok(all);
            }

            var result = await _workerService.SearchByNameAsync(name);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _workerService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [Authorize(Roles = $"{nameof(Roles.SuperAdmin)},{nameof(Roles.Admin)}")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] WorkerCreateDto dto)
        {
            await _workerService.CreateAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] WorkerUpdateDto dto)
        {
            await _workerService.UpdateAsync(id, dto);
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _workerService.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}