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
    }
}
