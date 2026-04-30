using GestorDeTareas.Application.Services;
using GestorDeTareas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PARTE 1: registrar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskManagerContext>
(options =>
options.UseSqlServer(
builder.Configuration
.GetConnectionString("GestorTareas")
)
);

// 2. Repositorio — cuando alguien pida ITareaRepositorio
// dar un TareaRepositorioEF
builder.Services.AddScoped<ITaskRepository, TaskRepositoryEF>();

// 3. Servicio
builder.Services.AddScoped<TaskManagerService>();

var app = builder.Build();

// PARTE 2: configurar el pipeline de peticiones
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); // arranca el servidor y se queda escuchando