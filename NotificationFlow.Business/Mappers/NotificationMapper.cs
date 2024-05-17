using NotificationFlow.Business.Command;
using NotificationFlow.Business.Entity;

namespace NotificationFlow.Business.Mappers
{
    public static class NotificationMapper
    {
        public static Notification NotificationCommandToNotificationGeneral(NotificationCommand userRequest)
        {
            return new Notification
            {
                Title = userRequest.Title,
                Description = userRequest.Description,
                IsGeneral = userRequest.IsGeneral
            };
        }

        public static Notification NotificationCommandToNotificationSpecific(NotificationCommand userRequest)
        {
            return new Notification
            {
                Title = userRequest.Title,
                Description = userRequest.Description,
                IsGeneral = userRequest.IsGeneral,
                NotificationUsers = new List<NotificationUser>
                {
                    new NotificationUser
                    {
                        UserId = userRequest.UserId,
                        IsRead = false
                    }
                }
            };
        }
    }
}
