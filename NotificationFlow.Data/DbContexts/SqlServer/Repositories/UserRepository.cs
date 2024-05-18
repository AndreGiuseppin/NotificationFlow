using Microsoft.EntityFrameworkCore;
using NotificationFlow.Business.Entity;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Data.Database.SqlServer;

namespace NotificationFlow.Data.DbContexts.SqlServer.Repositories
{
    public class UserRepository(ApplicationDbContext db) : IUserRepository
    {
        private readonly ApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task Post(User user)
        {
            await _db.Set<User>().AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User> Get(int userId)
        {
            var user = await _db.Set<User>()
                .Where(x => x.Id == userId)
                .Include(x => x.NotificationPreference)
                .Include(x => x.NotificationUsers)
                    .ThenInclude(x => x.Notification)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task PatchNotificationPreferences(int userId, bool ReceiveGeneralNotifications, bool ReceiveSpecificNotifications)
        {
            var user = await Get(userId);

            user.NotificationPreference.ReceiveGeneralNotifications = ReceiveGeneralNotifications;
            user.NotificationPreference.ReceiveSpecificNotifications = ReceiveSpecificNotifications;

            await _db.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersWithGeneralNotificationPreferencesEnabled()
        {
            var users = await _db.Set<User>().Include(x => x.NotificationPreference)
                .Where(x => x.NotificationPreference.ReceiveGeneralNotifications == true).ToListAsync();

            return users;
        }
    }
}
