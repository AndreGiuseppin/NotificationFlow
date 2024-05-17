using NotificationFlow.Business.Entity;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Data.Database.SqlServer;

namespace NotificationFlow.Data.DbContexts.SqlServer.Repositories
{
    public class NotificationRepository : Business.Interfaces.Repositories.INotificationRepository
    {
        private readonly ApplicationDbContext _db;

        public NotificationRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Notification> Post(Notification notification)
        {
            await _db.Set<Notification>().AddAsync(notification);
            await _db.SaveChangesAsync();

            return notification;
        }
    }
}
