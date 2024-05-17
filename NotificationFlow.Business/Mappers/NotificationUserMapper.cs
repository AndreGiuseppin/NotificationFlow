using NotificationFlow.Business.Entity;

namespace NotificationFlow.Business.Mappers
{
    public static class NotificationUserMapper
    {
        public static NotificationUser ToNotification(int notificationId, int userId)
        {
            return new NotificationUser
            {
                NotificationId = notificationId,
                UserId = userId,
                IsRead = false
            };
        }
    }
}
