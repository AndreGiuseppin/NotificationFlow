using NotificationFlow.Business.Command;

namespace NotificationFlow.Business.Models
{
    public class NotificationRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsGeneral { get; set; }
        public int UserId { get; set; }
        public bool SendPushNotification { get; set; }
        public bool SendEmailNotification { get; set; }
        public bool SendSmsNotification { get; set; }

        public static implicit operator NotificationCommand(NotificationRequest request)
        {
            return new NotificationCommand
            {
                Title = request.Title,
                Description = request.Description,
                IsGeneral = request.IsGeneral,
                UserId = request.UserId,
                SendPushNotification = request.SendPushNotification,
                SendEmailNotification = request.SendEmailNotification,
                SendSmsNotification = request.SendSmsNotification
            };
        }
    }
}
