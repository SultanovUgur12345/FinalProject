using FinalProjectMVC.Services.Interfaces;
using FinalProjectMVC.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("token") ?? Request.Cookies["jwt_token"];
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction("Login", "Account", new { area = "" });

            var callerRole = User.FindFirstValue(ClaimTypes.Role) ?? "Admin";

            var users = await _accountService.GetAllUsersAsync(token, callerRole);

            // Yalniz Admin ve Member-leri goster
            var filtered = users
                .Where(u => u.Role == "Admin" || u.Role == "Member")
                .ToList();

            ViewBag.CallerRole = callerRole;

            return View(filtered);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string email)
        {
            var token = HttpContext.Session.GetString("token") ?? Request.Cookies["jwt_token"];
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction("Login", "Account", new { area = "" });

            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction(nameof(Index));

            var callerRole = User.FindFirstValue(ClaimTypes.Role) ?? "Admin";

            var users = await _accountService.SearchUsersByEmailAsync(email, token, callerRole);

            var filtered = users
                .Where(u => u.Role == "Admin" || u.Role == "Member")
                .ToList();

            ViewBag.CallerRole = callerRole;
            ViewBag.SearchEmail = email;

            return View("Index", filtered);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(AssignRoleVM model)
        {
            var token = HttpContext.Session.GetString("token") ?? Request.Cookies["jwt_token"];
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction("Login", "Account", new { area = "" });

            var result = await _accountService.AssignRoleAsync(model, token);

            if (!result.success)
                TempData["UserError"] = result.message;
            else
                TempData["UserSuccess"] = result.message;

            return RedirectToAction(nameof(Index));
        }
    }
}
