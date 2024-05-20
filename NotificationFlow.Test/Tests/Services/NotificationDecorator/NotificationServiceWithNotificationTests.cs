using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using NotificationFlow.Business.Command;
using NotificationFlow.Business.Entity;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Services.NotificationDecorator;
using NotificationFlow.Test.Attributes;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace NotificationFlow.Test.Tests.Services.NotificationDecorator
{
    public class NotificationServiceWithNotificationTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void NotificationServiceWithNotificationTests_GuardClause(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(NotificationServiceWithNotification).GetConstructors());
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task ProcessAsync_WhenNotificationIsSpecific_ShouldCallNext
            (
                [Frozen] INotificationServiceCommand next,
                [Frozen] INotificationRepository notificationRepository,
                NotificationServiceWithNotification sut,
                NotificationCommand command,
                Task task
            )
        {
            command.IsGeneral = false;

            next.ProcessAsync(command).Returns(task);

            await sut.ProcessAsync(command);

            await notificationRepository.DidNotReceive().Post(Arg.Any<Notification>());
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task ProcessAsync_WhenExceptionOccurs_ShouldCallNothing
            (
                [Frozen] INotificationServiceCommand next,
                [Frozen] INotificationRepository notificationRepository,
                NotificationServiceWithNotification sut,
                NotificationCommand command,
                Exception ex
            )
        {
            command.IsGeneral = true;

            notificationRepository.Post(Arg.Any<Notification>()).Throws(ex);

            await sut.ProcessAsync(command);

            await next.DidNotReceive().ProcessAsync(command);
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task ProcessAsync_WhenNotificationIsGeneral_ShouldCallNext
            (
                [Frozen] INotificationServiceCommand next,
                [Frozen] INotificationRepository notificationRepository,
                NotificationServiceWithNotification sut,
                NotificationCommand command,
                Task task
            )
        {
            command.IsGeneral = true;

            next.ProcessAsync(command).Returns(task);

            await sut.ProcessAsync(command);

            await notificationRepository.Received(1).Post(Arg.Any<Notification>());
            await next.Received(1).ProcessAsync(command);
        }
    }
}
