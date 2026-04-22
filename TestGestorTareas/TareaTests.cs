using GestorDeTareas;

namespace TestGestorTareas
{
    public class TareaTests
    {
        private Tarea _tareaTest;

        [SetUp]
        public void Setup()
        {
            _tareaTest = new Bug("Titulo Test 001", Tarea.Prioridad.Baja, DateTime.Today.AddDays(1), null);
        }

        [Test]
        public void IniciarTarea()
        {
            // 2. ACT
            _tareaTest.Iniciar();

            // 3. ASSERT
            Assert.That(_tareaTest.EstadoTarea, Is.EqualTo(Tarea.Estado.EnProgreso));
        }
        [Test]
        public void CompletarTarea()
        {
            // 2. ACT
            _tareaTest.Completar();

            // 3. ASSERT
            Assert.That(_tareaTest.EstadoTarea, Is.EqualTo(Tarea.Estado.Completada));
        }
        [Test]
        public void CancelarTarea()
        {

            // 2. ACT
            _tareaTest.Cancelar(_tareaTest.MotivoCancelacion);
            _tareaTest.Cancelar(_tareaTest.Descripcion);

            // 3. ASSERT
            Assert.That(_tareaTest.EstadoTarea, Is.EqualTo(Tarea.Estado.Cancelada));
        }
        [Test]
        public void EstaVencida()
        {
            // 2. ACT
            _tareaTest.EstaVencida();

            // 3. ASSERT
            Assert.That(_tareaTest.EstaVencida(), Is.False);
        }
        [Test]
        public void CalcularTiempoRestante()
        {
            // 2. ACT
            _tareaTest.CalcularTiempoRestante();

            // 3. ASSERT
            Assert.That(_tareaTest.CalcularTiempoRestante(), Is.GreaterThan(0));
        }
    }
}
