using NotificationFlow.Business.Entity;

namespace NotificationFlow.Business.Mappers
{
    public static class NotificationUserMapper
    {
        public static NotificationUser ToNotificationUser(int notificationId, int userId)
        {
            return new NotificationUser
            {
                NotificationId = notificationId,
                UserId = userId,
                IsRead = false
            };
        }

        public static List<NotificationUser> ToNotificationUsers(int notificationId, List<int> usersId)
        {
            var notificationUsers = usersId.Select(x => new NotificationUser
            {
                NotificationId = notificationId,
                UserId = x,
                IsRead = false
            }).ToList();

            return notificationUsers;
        }
    }
}
