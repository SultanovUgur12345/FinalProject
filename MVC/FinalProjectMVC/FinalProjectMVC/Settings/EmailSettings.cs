namespace FinalProjectMVC.Settings
{
    public class EmailSettings
    {
        public string FromName { get; set; } = null!;
        public string FromEmail { get; set; } = null!;
        public string SmtpServer { get; set; } = null!;
        public int Port { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
