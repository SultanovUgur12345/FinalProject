namespace FinalProjectApi.Settings
{
    public class EmailSettings
    {
        public string FromName { get; set; }

        public string FromEmail { get; set; }

        public string SmtpServer { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
