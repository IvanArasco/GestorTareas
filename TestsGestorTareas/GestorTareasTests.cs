using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Domain.Entities;

namespace TestGestorTareas
{
    public class Tests
    {
        private GestorTareas _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new GestorTareas();
        }

        [Test]
        public void ObtenerTareas_TresTareasAgregadas_RetornaTresElementos() // Crear 3 y agregarlas.
        {
            // 1. ARRANGE
            int tareasAlInicio = _sut.ObtenerTareas().Count();

            var t1 = new TareaDto("Tarea Test 1", TareaDto.Prioridad.Alta, DateTime.Today.AddDays(1));
            var t2 = new TareaDto("Tarea Test 2", TareaDto.Prioridad.Baja, DateTime.Today.AddDays(5));
            var t3 = new TareaDto("Tarea Test 3", TareaDto.Prioridad.Urgente, DateTime.Today.AddDays(2));

            // 2. ACT
            _sut.AgregarTarea(t1);
            _sut.AgregarTarea(t2);
            _sut.AgregarTarea(t3);

            var resultado = _sut.ObtenerTareas();

            // 3. ASSERT
            // El resultado final debe ser las iniciales + las 3 que acabamos de meter
            int esperado = tareasAlInicio + 3;

            Assert.That(resultado.Count(), Is.EqualTo(esperado),
                $"El gestor debería tener {esperado} tareas en total.");

            // Verificamos que las tareas añadidas son exactamente las que creamos
            Assert.That(resultado, Contains.Item(t1));
            Assert.That(resultado, Contains.Item(t2));
            Assert.That(resultado, Contains.Item(t3));
        }

        [Test]
        public void ObtenerPorId_TareaExistente_RetornaTareaCorrecta()
        {

            // Arrange
            TareaDto t1 = new TareaDto("Titulo Test 001", TareaDto.Prioridad.Baja, DateTime.Today.AddDays(1));
            _sut.AgregarTarea(t1);

            // Act
            var resultado = _sut.ObtenerTareaId(t1.Id);

            // Assert
            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.Id, Is.EqualTo(t1.Id));
        }

    }
}