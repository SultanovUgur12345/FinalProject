using FinalProjectMVC.Services.Interfaces;
using FinalProjectMVC.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.RegisterAsync(model);

            if (!result.success)
            {
                ModelState.AddModelError("", result.message);
                return View(model);
            }

            ViewBag.SuccessMessage = "Mailinize baxin.";
            ModelState.Clear();

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.LoginAsync(model);

            if (!result.success)
            {
                ModelState.AddModelError("", result.message);
                return View(model);
            }

            Response.Cookies.Append("jwt_token", result.token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            HttpContext.Session.SetString("UserName", result.userName);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt_token");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.ForgotPasswordAsync(model);

            if (!result.success)
            {
                ModelState.AddModelError("", result.message);
                return View(model);
            }

            ViewBag.SuccessMessage = "Mailinize reset linki gonderildi.";
            ModelState.Clear();

            return View();
        }










        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPasswordVM
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.ResetPasswordAsync(model);

            if (!result.success)
            {
                ModelState.AddModelError("", result.message);
                return View(model);
            }

            TempData["Success"] = "Sifre ugurla yenilendi. Indi login ede bilersiniz.";
            return RedirectToAction(nameof(Login));
        }
    }
}