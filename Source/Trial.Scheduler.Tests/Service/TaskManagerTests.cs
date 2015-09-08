using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moq;
using NUnit.Framework;
using Shouldly;
using Trial.Scheduler.Service.Tasks;

namespace Trial.Scheduler.Tests.Service
{
    [TestFixture]
    public class TaskManagerTests
    {
        private TaskManager _taskManager;

        [SetUp]
        public void Setup()
        {
            _taskManager = new TaskManager();
        }

        [TearDown]
        public void FixtureShutdown()
        {
            _taskManager.Dispose();
        }

        [Test]
        public void ShouldAddTasksChronologically()
        {
            // arrange
            var now = DateTime.Now;
            var task1 = Mock.Of<ITask>(x => x.NextTime == now.AddHours(1));
            var task2 = Mock.Of<ITask>(x => x.NextTime == now.AddHours(2));
            var task3 = Mock.Of<ITask>(x => x.NextTime == now.AddHours(3));
            var task4 = Mock.Of<ITask>(x => x.NextTime == now.AddHours(4));

            // act
            _taskManager.AddTask(task3);
            _taskManager.AddTask(task2);
            _taskManager.AddTask(task4);
            _taskManager.AddTask(task1);

            // assert
            var tasks = _taskManager.GetTasks().ToArray();
            tasks[0].ShouldBeSameAs(task1);
            tasks[1].ShouldBeSameAs(task2);
            tasks[2].ShouldBeSameAs(task3);
            tasks[3].ShouldBeSameAs(task4);
        }

        [Test]
        public void ShouldShiftRepeatedTasks()
        {
            // arrange
            DateTime nextTime = DateTime.Now.AddSeconds(4);
            var task = new Mock<ITask>();
            task.SetupGet(x => x.NextTime).Returns(() => nextTime);
            task.Setup(x => x.UpdateNextTime()).Callback(() =>
            {
                nextTime = nextTime.AddDays(1);
                task.Raise(x => x.NextTimeChanged += null, EventArgs.Empty);
            });

            // act
            _taskManager.AddTask(task.Object);
            _taskManager.Run();
            Thread.Sleep(TimeSpan.FromSeconds(15));

            // assert
            task.Verify(x => x.Execute(), Times.Once);
        }

        [Test]
        public void ShouldRemoveTask()
        {
            // arrange
            var task1 = Mock.Of<ITask>(x => x.NextTime == DateTime.Now.AddHours(1));
            var task2 = Mock.Of<ITask>(x => x.NextTime == DateTime.Now.AddHours(2));

            // act
            _taskManager.Run();
            _taskManager.AddTask(task1);
            _taskManager.AddTask(task2);
            Assert.AreEqual(_taskManager.GetTaskCount(), 2);

            _taskManager.RemoveTask(task1);
            

            // assert
            _taskManager.GetTaskCount().ShouldBe(1);
            _taskManager.GetTasks().ShouldContain(task2);
        }

        [Test]
        public void ShouldDeleteObsoleteTask()
        {
            // arrange
            var task = new Mock<ITask>();
            task.SetupGet(x => x.NextTime).Returns(DateTime.Now.AddSeconds(3));

            // act
            _taskManager.Run();
            _taskManager.AddTask(task.Object);
            Thread.Sleep(5000);

            // assert
            task.Verify(x => x.Execute());
            _taskManager.GetTasks().ShouldNotContain(task.Object);
        }
    }
}