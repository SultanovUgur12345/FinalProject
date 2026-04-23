using AutoMapper;
using FinalProjectApi.DTOs.Account;
using FinalProjectApi.Helpers.Enums;
using FinalProjectApi.Models;
using FinalProjectApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FinalProjectApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IJwtService jwtService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<ResponceDto> RegisterAsync(RegisterDto dto, string scheme, string host)
        {
            var existByUserName = await _userManager.FindByNameAsync(dto.UserName);
            if (existByUserName != null)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Bu username artiq movcuddur"
                };
            }

            var existByEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (existByEmail != null)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Bu email artiq movcuddur"
                };
            }

            var user = _mapper.Map<AppUser>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = string.Join(" | ", result.Errors.Select(x => x.Description))
                };
            }

            var superAdminRole = nameof(Roles.Member);
            await EnsureRoleExistsAsync(superAdminRole);
            await _userManager.AddToRoleAsync(user, superAdminRole);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);

            var confirmUrl = $"{scheme}://{host}/api/account/confirm-email?userId={user.Id}&token={encodedToken}";

            await _emailService.SendConfirmEmailAsync(user.Email, user.FullName ?? user.UserName, confirmUrl);

            return new ResponceDto
            {
                IsSuccess = true,
                Message = "Qeydiyyat ugurla tamamlandi. Email tesdiq linki gonderildi.",
                ConfirmUrl = confirmUrl,
                UserEmail = user.Email,
                UserFullName = user.FullName ?? user.UserName
            };
        }

        public async Task<ResponceDto> ConfirmEmailAsync(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "UserId ve ya token bosdur"
                };
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Istifadeci tapilmadi"
                };
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = string.Join(" | ", result.Errors.Select(x => x.Description))
                };
            }

            return new ResponceDto
            {
                IsSuccess = true,
                RedirectUrl = "http://localhost:5015/Account/Login?confirmed=true"
            };
        }

        public async Task<ResponceDto> LoginAsync(LoginDto dto)
        {
            AppUser user;

            if (dto.UserNameOrEmail.Contains("@"))
                user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);
            else
                user = await _userManager.FindByNameAsync(dto.UserNameOrEmail);

            if (user == null)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Username/email ve ya password sehvdir"
                };
            }

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Username/email ve ya password sehvdir"
                };
            }

            if (!user.EmailConfirmed)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Zehmet olmasa emailinizi tesdiq edin"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var (token, expireDate) = _jwtService.GenerateToken(user, roles);

            return new ResponceDto
            {
                IsSuccess = true,
                Message = "Login ugurludur",
                Token = token,
                ExpireDate = expireDate,
                UserName = user.UserName
            };
        }

        public async Task<ResponceDto> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return new ResponceDto
                {
                    IsSuccess = true,
                    Message = "Eger bu email movcuddursa, reset linki gonderildi."
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);

            var resetUrl = $"http://localhost:5015/Account/ResetPassword?email={user.Email}&token={encodedToken}";

            await _emailService.SendResetPasswordEmailAsync(user.Email, user.FullName ?? user.UserName, resetUrl);

            return new ResponceDto
            {
                IsSuccess = true,
                Message = "Eger bu email movcuddursa, reset linki gonderildi.",
                ResetUrl = resetUrl,
                UserEmail = user.Email,
                UserFullName = user.FullName ?? user.UserName
            };
        }

        public async Task<ResponceDto> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Istifadeci tapilmadi"
                };
            }

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            if (!result.Succeeded)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = string.Join(" | ", result.Errors.Select(x => x.Description))
                };
            }

            return new ResponceDto
            {
                IsSuccess = true,
                Message = "Sifre ugurla yenilendi"
            };
        }








        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

