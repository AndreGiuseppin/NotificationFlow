using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using FluentAssertions;
using NotificationFlow.Business.Entity;
using NotificationFlow.Business.Enums;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Models;
using NotificationFlow.Business.Services;
using NotificationFlow.Test.Attributes;
using NSubstitute;

namespace NotificationFlow.Test.Tests.Services
{
    public class UserServiceTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void UserServiceTests_GuardClause(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(UserService).GetConstructors());
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task Post_WhenUserRequest_ShouldPostUser
            (
                [Frozen] IUserRepository userRepository,
                UserService sut,
                UserRequest request,
                Task task
            )
        {
            var user = new User
            {
                Name = request.Name,
                Document = request.Document,
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        ContactType = (int)ContactTypeEnum.Email,
                        Value = request.Email
                    },
                    new Contact
                    {
                        ContactType = (int)ContactTypeEnum.Phone,
                        Value = request.Phone
                    }
                },
                NotificationPreference = new NotificationPreference
                {
                    ReceiveGeneralNotifications = true,
                    ReceiveSpecificNotifications = true

                }
            };

            userRepository.Post(user).Returns(task);

            await sut.Post(request);

            await userRepository.Received(1).Post(Arg.Any<User>());
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task PatchNotificationPreferences_WhenRequest_ShouldPatch
            (
                [Frozen] IUserRepository userRepository,
                UserService sut,
                UserNotificationPreferencesRequest request,
                int userId,
                Task task
            )
        {
            userRepository.PatchNotificationPreferences(userId, request.ReceiveGeneralNotifications,
                request.ReceiveSpecificNotifications).Returns(task);

            await sut.PatchNotificationPreferences(userId, request);

            await userRepository.Received(1).PatchNotificationPreferences(userId, request.ReceiveGeneralNotifications,
                request.ReceiveSpecificNotifications);
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task GetUserNotifications_WhenUserNotExists_ShouldReturnNullObject
            (
                [Frozen] IUserRepository userRepository,
                UserService sut,
                int userId
            )
        {
            userRepository.Get(userId).Returns(null as User);

            var result = await sut.GetUserNotifications(userId);

            result.GeneralNotifications.Should().BeNull();
            result.SpecificNotification.Should().BeNull();
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task GetUserNotifications_WhenUserExists_ShouldReturnNotificationsObject
            (
                [Frozen] IUserRepository userRepository,
                UserService sut,
                int userId,
                User user
            )
        {
            user.NotificationUsers[0].Notification.IsGeneral = true;
            user.NotificationUsers[0].Notification.Title = "Title 1";

            user.NotificationUsers[1].Notification.IsGeneral = false;
            user.NotificationUsers[1].Notification.Title = "Title 2";

            userRepository.Get(userId).Returns(user);

            var result = await sut.GetUserNotifications(userId);

            result.GeneralNotifications[0].TitleNotification.Should().Be("Title 1");
            result.SpecificNotification[0].TitleNotification.Should().Be("Title 2");
        }
    }
}
