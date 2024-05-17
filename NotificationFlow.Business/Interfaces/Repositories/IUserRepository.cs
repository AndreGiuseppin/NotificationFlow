using NotificationFlow.Business.Entity;

namespace NotificationFlow.Business.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Post(User user);
        Task<User> Get(int userId);
    }
}
