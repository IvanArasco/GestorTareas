using GestorDeTareas.Application.Dtos;
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
            _taskTest.CalcRemainingDays();

            // 3. ASSERT
            Assert.That(_taskTest.CalcRemainingDays(), Is.GreaterThan(0));
        }

        [Test]
        public void GetTasks_AddThree_ReturnSix() // Crear 3 y agregarlas.
        {
            // 1. ARRANGE
            int tasksAtStart = _sut.GetTasks().Count();

            var t1 = new TaskRequestDto("Tarea Test 1", Priority.High, DateTime.Today.AddDays(1));
            var t2 = new TaskRequestDto("Tarea Test 2", Priority.Low, DateTime.Today.AddDays(5));
            var t3 = new TaskRequestDto("Tarea Test 3", Priority.Critical, DateTime.Today.AddDays(2));

            // 2. ACT
            _sut.AddTask(t1);
            _sut.AddTask(t2);
            _sut.AddTask(t3);

            var result = _sut.GetTasks();

            // 3. ASSERT
            // El resultado final debe ser las iniciales + las 3 que acabamos de meter
            int expected = tasksAtStart + 3;

            Assert.That(result.Count(), Is.EqualTo(expected),
                $"El gestor debería tener {expected} tareas en total.");

            // Verificamos que las tareas añadidas son exactamente las que creamos
            Assert.That(result, Contains.Item(t1));
            Assert.That(result, Contains.Item(t2));
            Assert.That(result, Contains.Item(t3));
        }

        [Test]
        public void GetTaskById_ReturnMatchTask()
        {

            // Arrange
            TaskRequestDto t1 = new TaskRequestDto("Titulo Test 001", Priority.Low, DateTime.Today.AddDays(1));
            _sut.AddTask(t1);

            // Act
            var Result = _sut.GetTaskById(t1.Id);

            // Assert
            Assert.That(Result, Is.Not.Null);
            Assert.That(Result.Id, Is.EqualTo(t1.Id));
        }
    }
}
