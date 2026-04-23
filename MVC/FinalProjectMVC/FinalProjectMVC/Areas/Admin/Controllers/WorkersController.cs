using FinalProjectMVC.ViewModels.Worker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IWorkerService;

namespace FinalProjectMVC.Areas.Admin.Controllers;

[Authorize(Roles = "SuperAdmin,Admin")]
public class WorkersController : AdminBaseController
{
    private readonly IWorkerApiService _service;

    public WorkersController(IWorkerApiService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
    {
        var data = await _service.GetPagedAsync(page, pageSize);
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string name)
    {
        var data = string.IsNullOrWhiteSpace(name)
            ? await _service.GetAllAsync()
            : await _service.SearchByNameAsync(name);

        return Json(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetPage([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var data = await _service.GetPagedAsync(page, pageSize);
        return Json(data);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var data = await _service.GetByIdAsync(id);
        if (data == null) return NotFound();

        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WorkerCreateVM dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var data = await _service.GetByIdForEditAsync(id);
        return View(data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, WorkerEditVM dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        dto.Id = id;

        await _service.UpdateAsync(id, dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
