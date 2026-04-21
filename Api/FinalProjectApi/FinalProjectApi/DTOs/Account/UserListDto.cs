namespace FinalProjectApi.DTOs.Account
{
    public class UserListDto
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool CanChangeRole { get; set; }
    }
}
