﻿namespace NotificationFlow.Business.Entity
{
    public class Notification : Entity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool SendToAll { get; set; } = false;

        public List<NotificationUser> NotificationUsers { get; set; }
    }
}
