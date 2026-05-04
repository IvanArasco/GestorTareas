using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Domain.Enums;

namespace TestGestorTareas
{
    public class Tests
    {
        private TaskManager _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TaskManager();
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