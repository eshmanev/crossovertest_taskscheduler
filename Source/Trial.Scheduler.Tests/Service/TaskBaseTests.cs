using System;
using System.Threading;
using Moq;
using NUnit.Framework;
using Shouldly;
using Trial.Scheduler.Service.Tasks;

namespace Trial.Scheduler.Tests.Service
{
    [TestFixture]
    public class TaskBaseTests
    {
        private TaskBase _task;

        [SetUp]
        public void Setup()
        {
            _task = Mock.Of<TaskBase>();
        }

        [Test]
        public void ShouldUpdateNextTimeWhenTriggerIsAdded()
        {
            var trigger = new ExactTimeTrigger(DateTime.Now.AddHours(2));
            _task.AddTrigger(trigger);
            _task.NextTime.ShouldBe(trigger.NextTime);
        }

        [Test]
        public void ShouldRearrangeTriggersOnAdd()
        {
            var later = new ExactTimeTrigger(DateTime.Now.AddHours(5));
            var earlier = new ExactTimeTrigger(DateTime.Now.AddHours(2));

            _task.AddTrigger(later);
            _task.AddTrigger(earlier);

            _task.NextTime.ShouldBe(earlier.NextTime);
        }

        [Test]
        public void ShouldRearrangeTriggersOnUpdate()
        {
            // arrange
            var now = DateTime.Now;
            var daily = new DailyTrigger(new TimeSpan(now.TimeOfDay.Hours, now.TimeOfDay.Minutes, now.TimeOfDay.Seconds).Add(TimeSpan.FromSeconds(5)));
            var exact = new ExactTimeTrigger(now.AddSeconds(3));

            // act
            _task.AddTrigger(daily);
            _task.AddTrigger(exact);
            Assert.AreEqual(exact.NextTime, _task.NextTime);

            Thread.Sleep(10000);
            _task.UpdateNextTime();

            // assert
            _task.NextTime.ShouldBe(daily.NextTime);
        }
    }
}