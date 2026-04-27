using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Domain.Enums;
using Task = GestorDeTareas.Domain.Entities.Task;

namespace TestGestorTareas
{
    public class TaskTests
    {
        private Task _taskTest;

        [SetUp]
        public void Setup()
        {
            _taskTest = new Bug("Titulo Test 001", Priority.Low, DateTime.Today.AddDays(1), null);
        }

        [Test]
        public void StartTask()
        {
            // 2. ACT
            _taskTest.Start();

            // 3. ASSERT
            Assert.That(_taskTest.TaskStatus, Is.EqualTo(Status.InProgress));
        }
        [Test]
        public void CompleteTask()
        {
            // 2. ACT
            _taskTest.Complete();

            // 3. ASSERT
            Assert.That(_taskTest.TaskStatus, Is.EqualTo(Status.Completed));
        }
        [Test]
        public void CancelTask()
        {
            // 2. ACT
            _taskTest.Cancel(_taskTest.CancellationReason);

            // 3. ASSERT
            Assert.That(_taskTest.TaskStatus, Is.EqualTo(Status.Cancelled));
        }
        [Test]
        public void HasExpired()
        {
            // 2. ACT
            _taskTest.HasExpired();

            // 3. ASSERT
            Assert.That(_taskTest.HasExpired(), Is.False);
        }
        [Test]
        public void CalcRemainingTime()
        {
            // 2. ACT
            _taskTest.CalcRemainingTime();

            // 3. ASSERT
            Assert.That(_taskTest.CalcRemainingTime(), Is.GreaterThan(0));
        }
    }
}
