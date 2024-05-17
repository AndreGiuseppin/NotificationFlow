using NotificationFlow.Business.Entity;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Data.Database.SqlServer;

namespace NotificationFlow.Data.DbContexts.SqlServer.Repositories
{
    public class NotificationUsersRepository : INotificationUsersRepository
    {
        private readonly ApplicationDbContext _db;

        public NotificationUsersRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task Post(NotificationUser notificationUser)
        {
            await _db.Set<NotificationUser>().AddAsync(notificationUser);
            await _db.SaveChangesAsync();
        }

        public async Task BulkPost(List<NotificationUser> notificationUsers)
        {
            await _db.Set<NotificationUser>().AddRangeAsync(notificationUsers);
            await _db.SaveChangesAsync();
        }
    }
}
