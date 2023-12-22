namespace Shared.Settings
{
    public class MailSettings
    {
        public string EmailFrom { get; set; } = string.Empty;
        public string SmtpHost { get; set; } =string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPass { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string ExchangeURL { get; set; } = string.Empty;
    }
}
