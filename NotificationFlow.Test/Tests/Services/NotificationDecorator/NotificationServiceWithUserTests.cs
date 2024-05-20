using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using NotificationFlow.Business.Command;
using NotificationFlow.Business.Entity;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Services.NotificationDecorator;
using NotificationFlow.Test.Attributes;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace NotificationFlow.Test.Tests.Services.NotificationDecorator
{
    public class NotificationServiceWithUserTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void NotificationServiceWithUserTests_GuardClause(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(NotificationServiceWithUser).GetConstructors());
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task ProcessAsync_WhenNotificationIsSpecificAndUserDoesntHavePreference_ShouldReturn
        (
                [Frozen] IUserRepository userRepository,
                NotificationServiceWithUser sut,
                NotificationCommand command,
                User user
            )
        {
            command.IsGeneral = false;
            user.NotificationPreference.ReceiveSpecificNotifications = false;

            userRepository.Get(command.UserId).Returns(user);

            await sut.ProcessAsync(command);

            userRepository.DidNotReceive().GetUsersWithGeneralNotificationPreferencesEnabled();
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task ProcessAsync_WhenNotificationIsSpecificAndUserHavePreference_ShouldPostNotification
        (
                [Frozen] IUserRepository userRepository,
                [Frozen] INotificationUsersRepository notificationUsersRepository,
                NotificationServiceWithUser sut,
                NotificationCommand command,
                User user
            )
        {
            command.IsGeneral = false;
            user.NotificationPreference.ReceiveSpecificNotifications = true;

            userRepository.Get(command.UserId).Returns(user);

            await sut.ProcessAsync(command);

            notificationUsersRepository.Received(1).Post(Arg.Any<NotificationUser>());
            userRepository.DidNotReceive().GetUsersWithGeneralNotificationPreferencesEnabled();
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task ProcessAsync_WhenNotificationIsGeneralAndDontHaveUsersWithPreference_ShouldReturn
        (
                [Frozen] IUserRepository userRepository,
                [Frozen] INotificationUsersRepository notificationUsersRepository,
                NotificationServiceWithUser sut,
                NotificationCommand command,
                List<User> users
            )
        {
            command.IsGeneral = true;
            users.RemoveRange(0, 3);

            userRepository.GetUsersWithGeneralNotificationPreferencesEnabled()
                .Returns(users);

            await sut.ProcessAsync(command);

            notificationUsersRepository.DidNotReceive().BulkPost(Arg.Any<List<NotificationUser>>());
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task ProcessAsync_WhenErrorOccurs_ShouldCallNothing
        (
                [Frozen] IUserRepository userRepository,
                NotificationServiceWithUser sut,
                NotificationCommand command,
                Exception ex
            )
        {
            userRepository.Get(command.UserId)
                .Throws(ex);

            await sut.ProcessAsync(command);

            userRepository.DidNotReceive().GetUsersWithGeneralNotificationPreferencesEnabled();
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task ProcessAsync_WhenNotificationIsGeneralAndHaveUsersWithPreference_ShouldBulkPost
        (
                [Frozen] IUserRepository userRepository,
                [Frozen] INotificationUsersRepository notificationUsersRepository,
                NotificationServiceWithUser sut,
                NotificationCommand command,
                List<User> users
            )
        {
            command.IsGeneral = true;
            users.ForEach(x => x.NotificationPreference.ReceiveGeneralNotifications = true);

            userRepository.GetUsersWithGeneralNotificationPreferencesEnabled()
                .Returns(users);

            await sut.ProcessAsync(command);

            notificationUsersRepository.Received(1).BulkPost(Arg.Any<List<NotificationUser>>());
        }
    }
}
