using NotificationFlow.Business.Entity;
using NotificationFlow.Business.Enums;
using NotificationFlow.Business.Models;

namespace NotificationFlow.Business.Mappers
{
    public static class UserMapper
    {
        public static User UserRequestToUser(UserRequest userRequest)
        {
            return new User
            {
                Name = userRequest.Name,
                Document = userRequest.Document,
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        ContactType = (int)ContactTypeEnum.Email,
                        Value = userRequest.Email
                    },
                    new Contact
                    {
                        ContactType = (int)ContactTypeEnum.Phone,
                        Value = userRequest.Phone
                    }
                },
                NotificationPreference = new NotificationPreference
                {
                    ReceiveGeneralNotifications = true,
                    ReceiveSpecificNotifications = true
                }
            };
        }

        public static GetUserNotificationsResponse UserToUserNotification(User user)
        {
            return new GetUserNotificationsResponse
            {
                GeneralNotifications = user.NotificationUsers.Where(x => x.Notification.IsGeneral == true).Select(x => new UserNotification
                {
                    NotificationUserId = x.Id,
                    TitleNotification = x.Notification.Title,
                    DescriptionNotification = x.Notification.Description,
                    IsRead = x.IsRead
                }).ToList(),
                SpecificNotification = user.NotificationUsers.Where(x => x.Notification.IsGeneral == false).Select(x => new UserNotification
                {
                    NotificationUserId = x.Id,
                    TitleNotification = x.Notification.Title,
                    DescriptionNotification = x.Notification.Description,
                    IsRead = x.IsRead
                }).ToList()
            };
        }
    };
}