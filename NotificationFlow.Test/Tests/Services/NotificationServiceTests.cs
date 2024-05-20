using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using NotificationFlow.Business.Interfaces.Producer;
using NotificationFlow.Business.Models;
using NotificationFlow.Business.Options;
using NotificationFlow.Business.Services;
using NotificationFlow.Test.Attributes;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace NotificationFlow.Test.Tests.Services
{
    public class NotificationServiceTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void NotificationServiceTests_GuardClause(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(NotificationService).GetConstructors());
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task Post_WhenScheduleIsNotNull_ShouldSchedule
        (
                [Frozen] IBackgroundJobClient backgroundJob,
                NotificationService sut,
                NotificationRequest request
            )
        {
            request.ScheduleTime = DateTime.UtcNow;

            await sut.Post(request);

            backgroundJob.Received(1).Create(
                Arg.Is<Job>(job => job.Method.Name == "ProduceAsync" && job.Args[0].Equals(request)),
                Arg.Any<ScheduledState>()
                );
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task Post_WhenExceptionOccurs_ShouldNotCallNothing
        (
                [Frozen] IKafkaProducer producer,
                [Frozen] IBackgroundJobClient backgroundJob,
                KafkaOptions kafkaOptions,
                NotificationService sut,
                NotificationRequest request,
                Task task,
                Exception ex
            )
        {
            request.ScheduleTime = null;

            producer.Producer(request, kafkaOptions.NotificationTopic)
                .Throws(ex);

            await sut.Post(request);

            backgroundJob.DidNotReceive().Create(
                Arg.Is<Job>(job => job.Method.Name == "ProduceAsync" && job.Args[0].Equals(request)),
                Arg.Any<ScheduledState>()
                );

            producer.DidNotReceive().Producer(request, kafkaOptions.NotificationTopic);
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task Post_WhenScheduleIsNull_ShouldProduce
        (
                [Frozen] IKafkaProducer producer,
                [Frozen] IBackgroundJobClient backgroundJob,
                KafkaOptions kafkaOptions,
                NotificationService sut,
                NotificationRequest request,
                Task task
            )
        {
            request.ScheduleTime = null;

            producer.Producer(request, kafkaOptions.NotificationTopic)
                .Returns(task);

            await sut.Post(request);

            backgroundJob.DidNotReceive().Create(
                Arg.Is<Job>(job => job.Method.Name == "ProduceAsync" && job.Args[0].Equals(request)),
                Arg.Any<ScheduledState>()
                );
        }
    }
}
