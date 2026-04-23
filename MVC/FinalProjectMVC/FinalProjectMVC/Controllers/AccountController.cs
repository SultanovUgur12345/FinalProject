using FinalProjectMVC.Services.Interfaces;
using FinalProjectMVC.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinalProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly string _apiBaseUrl;

        public AccountController(IAccountService accountService, IUserService userService, IConfiguration configuration)
        {
            _accountService = accountService;
            _userService = userService;
            _apiBaseUrl = (configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5147").TrimEnd('/');
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

            var profileData = await _userService.GetProfileAsync(result.token);
            if (!string.IsNullOrWhiteSpace(profileData?.ProfileImageUrl))
            {
                var imageUrl = profileData.ProfileImageUrl.StartsWith("http")
                    ? profileData.ProfileImageUrl
                    : $"{_apiBaseUrl}{profileData.ProfileImageUrl}";
                HttpContext.Session.SetString("ProfileImage", imageUrl);
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(result.token);
            var claims = jwtToken.Claims.Select(c => (c.Type == "role" || c.Type == ClaimTypes.Role) ? new Claim(ClaimTypes.Role, c.Value) : c).ToList();

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                    IsPersistent = false
                });

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt_token");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("ProfileImage");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> UserList()
        {
            var token = HttpContext.Session.GetString("token") ?? Request.Cookies["jwt_token"];
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction(nameof(Login));

            var callerRole = User.IsInRole("SuperAdmin") ? "SuperAdmin" : "Admin";
            var users = await _userService.GetAllUsersAsync(token, callerRole);
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AssignRole(string userId, string userName, string email, string currentRole)
        {
            var token = HttpContext.Session.GetString("token") ?? Request.Cookies["jwt_token"];
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction(nameof(Login));

            var model = new AssignRoleVM
            {
                UserId = userId,
                UserName = userName,
                Email = email,
                Role = currentRole
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AssignRole(AssignRoleVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = HttpContext.Session.GetString("token") ?? Request.Cookies["jwt_token"];
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction(nameof(Login));

            var result = await _userService.AssignRoleAsync(model, token);

            if (!result.success)
            {
                ModelState.AddModelError("", result.message);
                return View(model);
            }

            TempData["Success"] = result.message;
            return RedirectToAction(nameof(UserList));
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



        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var token = HttpContext.Session.GetString("token");

            if (string.IsNullOrWhiteSpace(token))
            {
                token = Request.Cookies["jwt_token"];
            }

            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction(nameof(Login));

            var profile = await _userService.GetProfileAsync(token);

            var sessionImage = HttpContext.Session.GetString("ProfileImage");
            string? profileImageUrl = null;

            if (!string.IsNullOrWhiteSpace(sessionImage))
                profileImageUrl = sessionImage;
            else if (!string.IsNullOrWhiteSpace(profile.ProfileImageUrl))
                profileImageUrl = profile.ProfileImageUrl.StartsWith("http")
                    ? profile.ProfileImageUrl
                    : $"{_apiBaseUrl}{profile.ProfileImageUrl}";

            var model = new UpdateProfileVM
            {
                FullName = profile.FullName,
                UserName = profile.UserName,
                ProfileImageUrl = profileImageUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UpdateProfileVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = HttpContext.Session.GetString("token");

            if (string.IsNullOrWhiteSpace(token))
            {
                token = Request.Cookies["jwt_token"];
            }

            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction(nameof(Login));

            var result = await _userService.UpdateProfileAsync(model, model.ProfileImage, token);

            if (!result.success)
            {
                ModelState.AddModelError("", result.message);
                return View(model);
            }

            TempData["Success"] = result.message;

            if (!string.IsNullOrWhiteSpace(result.userName))
                HttpContext.Session.SetString("UserName", result.userName);

            if (!string.IsNullOrWhiteSpace(result.profileImageUrl))
                HttpContext.Session.SetString("ProfileImage", result.profileImageUrl);

            return RedirectToAction(nameof(EditProfile));
        }
    }
}