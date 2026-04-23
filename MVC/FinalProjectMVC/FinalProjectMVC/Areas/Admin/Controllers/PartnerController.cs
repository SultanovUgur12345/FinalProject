using FinalProjectMVC.ViewModels.Partner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Areas.Admin.Controllers;

[Authorize(Roles = "SuperAdmin,Admin")]
public class PartnerController : AdminBaseController
{
    private readonly IPartnerApiService _service;

    public PartnerController(IPartnerApiService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PartnerCreateVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        await _service.CreateAsync(vm);
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
    public async Task<IActionResult> Edit(int id, PartnerEditVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        vm.Id = id;
        await _service.UpdateAsync(id, vm);
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
