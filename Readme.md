DEFINICIÓN DEL PROYECTO:

* Gestor de tareas en .NET (simulando una aplicación de gestión como Jira o Trello).
* Réplica de estructura con delegación de funciones entre capas (Controller - Service - Repository).
* Aplicación de los principios SOLID.

ESTRUCTURA DEL PROYECTO:

* Clase padre Task --> 4 clases hijas --> Bug, Improvement, NewFeature, RecurringTask.
* Clase User --> Auth con Jwt, Login y Register.
* Conexión a BD utilizando Entity Framework.
* Angular en el Front. Principios de Usabilidad y Accesibilidad.

ESTRUCTURA DE LAS CLASES: (Resumen)

* Task (clase padre):

  * ID: Autoincremental
  * Descripción (nullable): Detalles de interés (string)
  * Prioridad: Enumerado (Low, Medium, High, Critical)
  * Estado: Enumerado (Pending, InProgress, Completed, Cancelled)
  * Fecha de creación y finalización: DateTime
  * Métodos para iniciar, completar, cancelar, comprobar expiración, establecer prioridad...
* Bug:

  * Comportamiento esperado (nullable) y actual: string
* Improvement:

  * Funcionalidad afectada y beneficio esperado (nullable)
* New Feature:

  * Área de desarrollo: Enumerado (Backend, Frontend, BBDD)
* RecurringTask:

  * Próxima y última ejecución (nullable): DateTime
  * Frecuencia: Enumerado (Diaria, Semanal, Mensual)
  * Validación de la siguiente fecha de ejecución
* User (Clase para autenticación y autorización)

  * ID: Autoincremental
  * Name: string (Utilizado además para identificar a la persona responsable de la Tarea a la que va asociado).
  * PasswordHash: string (será Hasheada y se agregará una capa extra de seguridad con Salt).
  * BirthDate: DateOnly
  * IsAdmin: boolean
  * List<Task> Tasks: La lista de tareas asociadas al usuario.
  * Métodos para verificar el email (validación con expresión regular), así como la fecha de nacimiento.

