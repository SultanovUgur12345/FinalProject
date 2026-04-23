using FinalProjectApi.DTOs.Account;
using FinalProjectApi.Helpers.Enums;
using FinalProjectApi.Models;
using FinalProjectApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FinalProjectApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetProfileDto?> GetProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return new GetProfileDto
            {
                FullName = user.FullName ?? string.Empty,
                UserName = user.UserName ?? string.Empty,
                ProfileImage = user.ProfileImage
            };
        }

        public async Task<ResponceDto> UpdateProfileAsync(string userId, UpdateProfileDto dto, IFormFile? file, string? baseUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Istifadeci tapilmadi"
                };
            }

            if (!string.IsNullOrWhiteSpace(dto.FullName))
            {
                user.FullName = dto.FullName.Trim();
            }

            if (!string.IsNullOrWhiteSpace(dto.UserName))
            {
                var newUserName = dto.UserName.Trim();

                if (newUserName != user.UserName)
                {
                    var existUser = await _userManager.FindByNameAsync(newUserName);

                    if (existUser != null && existUser.Id != user.Id)
                    {
                        return new ResponceDto
                        {
                            IsSuccess = false,
                            Message = "Bu username artiq movcuddur"
                        };
                    }

                    user.UserName = newUserName;
                }
            }

            bool currentPasswordFilled = !string.IsNullOrWhiteSpace(dto.CurrentPassword);
            bool newPasswordFilled = !string.IsNullOrWhiteSpace(dto.NewPassword);

            if (currentPasswordFilled || newPasswordFilled)
            {
                if (!currentPasswordFilled || !newPasswordFilled)
                {
                    return new ResponceDto
                    {
                        IsSuccess = false,
                        Message = "Sifre deyismek ucun hem kohne, hem de yeni sifre daxil edilmelidir"
                    };
                }

                var passwordResult = await _userManager.ChangePasswordAsync(
                    user,
                    dto.CurrentPassword!,
                    dto.NewPassword!
                );

                if (!passwordResult.Succeeded)
                {
                    return new ResponceDto
                    {
                        IsSuccess = false,
                        Message = string.Join(", ", passwordResult.Errors.Select(x => x.Description))
                    };
                }
            }

            if (file != null && !string.IsNullOrWhiteSpace(baseUrl))
            {
                var folder = Path.Combine("wwwroot", "uploads", "users");
                Directory.CreateDirectory(folder);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);

                if (!string.IsNullOrWhiteSpace(user.ProfileImage))
                {
                    var oldPath = Path.Combine("wwwroot", user.ProfileImage.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (File.Exists(oldPath)) File.Delete(oldPath);
                }

                user.ProfileImage = $"/uploads/users/{fileName}";
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = string.Join(", ", updateResult.Errors.Select(x => x.Description))
                };
            }

            return new ResponceDto
            {
                IsSuccess = true,
                Message = "Profil ugurla yenilendi",
                UserName = user.UserName,
                ProfileImage = file != null && !string.IsNullOrWhiteSpace(baseUrl)
                    ? $"{baseUrl}/uploads/users/{Path.GetFileName(user.ProfileImage)}"
                    : null
            };
        }

        public async Task<List<UserListDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserListDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserListDto
                {
                    Id = user.Id,
                    FullName = user.FullName ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Role = roles.FirstOrDefault() ?? string.Empty,
                    CanChangeRole = roles.FirstOrDefault() != nameof(Roles.SuperAdmin)
                });
            }

            return result;
        }

        public async Task<PaginatedResult<UserListDto>> GetPagedUsersAsync(int page, int pageSize)
        {
            var allUsers = _userManager.Users
                .Where(u => u.Email != null)
                .OrderBy(u => u.UserName)
                .ToList();

            var totalCount = allUsers.Count;
            var paged = allUsers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new List<UserListDto>();
            foreach (var user in paged)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserListDto
                {
                    Id = user.Id,
                    FullName = user.FullName ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Role = roles.FirstOrDefault() ?? string.Empty,
                    CanChangeRole = roles.FirstOrDefault() != nameof(Roles.SuperAdmin)
                });
            }

            return new PaginatedResult<UserListDto>
            {
                Items = result,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<List<UserListDto>> SearchByFullNameAsync(string fullName)
        {
            var users = _userManager.Users
                .Where(u => u.FullName != null && u.FullName.Contains(fullName))
                .ToList();

            var result = new List<UserListDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserListDto
                {
                    Id = user.Id,
                    FullName = user.FullName ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Role = roles.FirstOrDefault() ?? string.Empty,
                    CanChangeRole = roles.FirstOrDefault() != nameof(Roles.SuperAdmin)
                });
            }

            return result;
        }

        public async Task<ResponceDto> AssignRoleAsync(AssignRoleDto dto, string callerRole = "SuperAdmin")
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user == null)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Istifadeci tapilmadi"
                };
            }

            var targetRoles = await _userManager.GetRolesAsync(user);
            var targetRole = targetRoles.FirstOrDefault() ?? string.Empty;

            if (targetRole == nameof(Roles.SuperAdmin))
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "SuperAdmin istifadecisinin rolu deyisile bilmez"
                };
            }

            if (callerRole == nameof(Roles.Admin) && targetRole != nameof(Roles.Member))
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Admin yalniz Member istifadecisinin rolunu deyise biler"
                };
            }

            if (callerRole == nameof(Roles.Admin) && dto.Role != Roles.Member)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Admin yalniz Member rolu teyinleye biler"
                };
            }

            if (dto.Role == Roles.SuperAdmin)
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "SuperAdmin rolu teyinlenile bilmez"
                };
            }

            if (!Enum.IsDefined(typeof(Roles), dto.Role))
            {
                return new ResponceDto
                {
                    IsSuccess = false,
                    Message = "Gecersiz rol"
                };
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await _userManager.AddToRoleAsync(user, dto.Role.ToString());

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
                Message = $"Rol ugurla teyinlendi: {dto.Role}"
            };
        }
    }
}
