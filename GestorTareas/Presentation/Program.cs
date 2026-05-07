using GestorDeTareas.Application.Services;
using GestorDeTareas.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PARTE 1: Registrar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<TaskManagerContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("GestorTareas")));

// PARTE 2. Registrar Repositorios
builder.Services.AddScoped<ITaskRepository, TaskRepositoryEF>();
builder.Services.AddScoped<IUserRepository, UserRepositoryEF>();

// PARTE 3. Registrar Servicios
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();

// Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => { /* configuración de diapositiva 6 */ });

var app = builder.Build();

// PARTE 4: Configurar el pipeline de peticiones
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run(); // arranca el servidor y se queda escuchando