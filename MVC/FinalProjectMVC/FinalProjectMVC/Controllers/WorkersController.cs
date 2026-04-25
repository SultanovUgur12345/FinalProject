using FinalProjectMVC.Services.Interfaces;
using FinalProjectMVC.ViewModels.Worker;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class WorkersController : Controller
    {
        private readonly IWorkerApiService _workerService;

        public WorkersController(IWorkerApiService workerService)
        {
            _workerService = workerService;
        }

        public async Task<IActionResult> Index()
        {
            var workers = await _workerService.GetAllAsync();
            return View(workers);
        }
    }
}
