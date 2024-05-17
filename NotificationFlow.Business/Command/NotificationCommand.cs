namespace NotificationFlow.Business.Command
{
    public class NotificationCommand
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsGeneral { get; set; }
        public int UserId { get; set; }
        public bool SendPushNotification { get; set; }
        public bool SendEmailNotification { get; set; }
        public bool SendSmsNotification { get; set; }
    }
}
