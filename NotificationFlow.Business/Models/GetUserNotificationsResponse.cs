namespace NotificationFlow.Business.Models
{
    public class GetUserNotificationsResponse
    {
        public List<UserNotification>? GeneralNotifications { get; set; }
        public List<UserNotification>? SpecificNotification { get; set; }
    }

    public class UserNotification
    {
        public int NotificationUserId { get; set; }
        public string TitleNotification { get; set; } = string.Empty;
        public string DescriptionNotification { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
}