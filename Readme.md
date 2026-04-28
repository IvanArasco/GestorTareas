DEFINICIÓN DEL PROYECTO:
- Gestor de tareas (simulando una aplicación de gestión como Jira o Trello)

ESTRUCTURA DEL PROYECTO:
- Clase padre Tarea --> 4 clases hijas --> Bug, Improvement, NewFeature, RecurringTask.
- Conexión a BD utilizando EntityFramework.

ESTRUCTURA DE LAS CLASES: (Resumen)
- Tarea (clase padre):
	- ID: Autoincremental
	- Descripción (nullable): Detalles de interés (string)
	- Prioridad: Enumerado (Low, Medium, High, Critical)
	- Estado: Enumerado (Pending, InProgress, Completed, Cancelled)
	- Fecha de creación y finalización: DateTime
	- Métodos para iniciar, completar, cancelar, comprobar expiración, establecer prioridad...
- Bug:
	- Comportamiento esperado (nullable) y actual: string
- Improvement:
	- Funcionalidad afectada y beneficio esperado (nullable)
- New Feature:
	- Área de desarrollo: Enumerado (Backend, Frontend, BBDD)
- RecurringTask:
	- Próxima y última ejecución (nullable): DateTime
	- Frecuencia: Enumerado (Diaria, Semanal, Mensual)
	- Validación de la siguiente fecha de ejecución
	

