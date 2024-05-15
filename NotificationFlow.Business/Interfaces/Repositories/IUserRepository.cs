using NotificationFlow.Business.Entity;

namespace NotificationFlow.Business.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Post(User user);
    }
}
