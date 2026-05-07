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
            _taskTest = new Bug(
              title: "Test Bug 001",
              taskPriority: Priority.Low,
              expirationDate: DateTime.Today.AddDays(5),
              userId: 1,
              actualBehaviour: "App crashes on login");
        }

        [Test]
        public void StartTask_Pending_ChagesStatusInProgress()
        {
            // 2. ACT
            _taskTest.Start();

            // 3. ASSERT
            Assert.That(_taskTest.TaskStatus, Is.EqualTo(Status.InProgress));
        }

        [Test]
        public void StartTask_Started_ThrowsInvalidOperationException()
        {
            // 2. ACT
            _taskTest.Start();

            // 3. ASSERT
            Assert.Throws<InvalidOperationException>(() => _taskTest.Start());
        }

        [Test]
        public void CompleteTask_InProgress_ChangeStatusCompleted()
        {
            // 2. ACT
            _taskTest.Start();
            _taskTest.Complete();

            // 3. ASSERT
            Assert.That(_taskTest.TaskStatus, Is.EqualTo(Status.Completed));
        }

        [Test]
        public void CompleteTask_Cancelled_ThrowsInvalidOperationException()
        {
            // 2. ACT
            _taskTest.Cancel("Random reason");

            // 3. ASSERT
            Assert.Throws<InvalidOperationException>(() => _taskTest.Complete());
        }

        [Test]
        public void CompleteTask_Completed_ThrowsInvalidOperationException()
        {

            // 2. ACT
            _taskTest.Start();
            _taskTest.Complete();

            // 3. ASSERT
            Assert.Throws<InvalidOperationException>(() => _taskTest.Complete());
        }

        [Test]
        public void CancelTask_Pending_ChangesStatusToCancelled()
        {
            // 2. ACT
            _taskTest.Cancel("Random reason");

            // 3. ASSERT
            Assert.That(_taskTest.TaskStatus, Is.EqualTo(Status.Cancelled));
        }

        [Test]
        public void CancelTask_Cancelled_ThrowsInvalidOperationException()
        {
            // 2. ACT
            _taskTest.Cancel("Random reason");

            // 3. ASSERT
            Assert.Throws<InvalidOperationException>(() => _taskTest.Cancel("Otro motivo"));
        }

        [Test]
        public void HasExpired_FutureExpirationDate_ReturnFalse()
        {
            // 2. ACT
            _taskTest.HasExpired();

            // 3. ASSERT
            Assert.That(_taskTest.HasExpired(), Is.False);
        }
        [Test]
        public void CalcRemainingDays_FutureExpirationDate_ReturnPositiveNumber()
        {
            // 2. ACT
            _taskTest.CalcRemainingDays();

            // 3. ASSERT
            Assert.That(_taskTest.CalcRemainingDays(), Is.GreaterThan(0));
        }
    }
}
