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
                }
            };
        }
    }
}
