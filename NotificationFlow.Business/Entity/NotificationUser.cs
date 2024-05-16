namespace NotificationFlow.Business.Entity
{
    public class NotificationUser : Entity
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public bool IsRead { get; set; } = false;

        public User Users { get; set; }
        public Notification Notification { get; set; }
    }
}
