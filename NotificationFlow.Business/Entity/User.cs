namespace NotificationFlow.Business.Entity
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;

        public List<Contact> Contacts { get; set; }
        public List<NotificationUser> NotificationUsers { get; set; }
    }
}
