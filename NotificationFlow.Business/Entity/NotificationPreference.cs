namespace NotificationFlow.Business.Entity
{
    public class NotificationPreference : Entity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool ReceiveGeneralNotifications { get; set; }
        public bool ReceiveSpecificNotifications { get; set; }

        public User User { get; set; }
    }
}
