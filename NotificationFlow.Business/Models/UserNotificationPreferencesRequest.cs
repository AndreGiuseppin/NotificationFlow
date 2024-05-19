using Microsoft.AspNetCore.Mvc;

namespace NotificationFlow.Business.Models
{
    public class UserNotificationPreferencesRequest
    {
        [FromBody] public bool ReceiveGeneralNotifications { get; set; }
        [FromBody] public bool ReceiveSpecificNotifications { get; set; }
    }
}
