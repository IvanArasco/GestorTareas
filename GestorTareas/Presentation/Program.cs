using GestorDeTareas.Application.Services;
using GestorDeTareas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PARTE 1: registrar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskContext>
(options =>
options.UseSqlServer(
builder.Configuration
.GetConnectionString("GestorTareas")
)
);

// 2. Repositorios
builder.Services.AddScoped<ITaskRepository, TaskRepositoryEF>();
builder.Services.AddScoped<IUserRepository, UserRepositoryEF>();

// 3. Servicios
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<UserService>();

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