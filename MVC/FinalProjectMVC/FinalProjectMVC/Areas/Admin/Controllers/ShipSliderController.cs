using FinalProjectMVC.ViewModels.ShipSlider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Areas.Admin.Controllers;

[Authorize(Roles = "SuperAdmin,Admin")]
public class ShipSliderController : AdminBaseController
{
    private readonly IShipSliderApiService _service;

    public ShipSliderController(IShipSliderApiService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAdminAsync();
        return View(data);
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
    public async Task<IActionResult> Create(ShipSliderCreateVM dto)
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
    public async Task<IActionResult> Edit(int id, ShipSliderEditVM dto)
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
