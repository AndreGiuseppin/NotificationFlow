using Microsoft.AspNetCore.Mvc;
using NotificationFlow.Business.Command;

namespace NotificationFlow.Business.Models
{
    public class GetUserNotificationsRequest
    {
        [FromRoute] public int Id { get; set; }

        public static implicit operator GetUserNotificationsCommand(GetUserNotificationsRequest request)
        {
            return new GetUserNotificationsCommand
            {
                UserId = request.Id
            };
        }
    }
}
