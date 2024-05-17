using Microsoft.EntityFrameworkCore;
using NotificationFlow.Business.Entity;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Data.Database.SqlServer;

namespace NotificationFlow.Data.DbContexts.SqlServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

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
    }
}
