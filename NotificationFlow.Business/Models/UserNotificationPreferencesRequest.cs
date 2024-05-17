namespace NotificationFlow.Business.Models
{
    public class UserNotificationPreferencesRequest
    {
        public int UserId { get; set; }
        public bool ReceiveGeneralNotifications { get; set; }
        public bool ReceiveSpecificNotifications { get; set; }
    }
}
