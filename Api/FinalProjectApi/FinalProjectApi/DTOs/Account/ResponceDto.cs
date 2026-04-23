namespace FinalProjectApi.DTOs.Account
{
    public class ResponceDto
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public DateTime? ExpireDate { get; set; }

        public string UserName { get; set; }

        public string RedirectUrl { get; set; }
        public string? ProfileImage { get; set; }

        public string? ConfirmUrl { get; set; }
        public string? UserEmail { get; set; }
        public string? UserFullName { get; set; }
        public string? ResetUrl { get; set; }
    }
}
