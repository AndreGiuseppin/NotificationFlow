namespace NotificationFlow.Business.Entity
{
    public class Contact : Entity
    {
        public int Id { get; set; }
        public int ContactType { get; set; }
        public string Value { get; set; } = string.Empty;
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
