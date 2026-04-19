namespace FinalProjectMVC.ViewModels.Account
{
    public class ResponceVM
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? UserName { get; set; }
        public string? RedirectUrl { get; set; }
    }
}
