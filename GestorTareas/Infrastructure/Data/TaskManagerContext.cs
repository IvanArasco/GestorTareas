using GestorDeTareas.Domain.Entities;
using Task = GestorDeTareas.Domain.Entities.Task;
using Microsoft.EntityFrameworkCore;

public class TaskManagerContext : DbContext
{
    // Cada DbSet representa una tabla en la BD
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Indicar a EF Core qué proveedor usar y cómo conectarse
        options.UseSqlServer(
     @"Server=(localdb)\MSSQLLocalDB;" +
     "Database=GestorTareas;" +
     "Trusted_Connection=True;" +
     "TrustServerCertificate=True;"
 );
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar el discriminador TPH
        modelBuilder.Entity<Task>()
        .HasDiscriminator<string>("TipoTarea")
        .HasValue<RecurringTask>("Recurrente")
        .HasValue<Bug>("Bug")
        .HasValue<Improvement>("Mejora")
        .HasValue<NewFeature>("Nueva funcionalidad");

        // Limitar longitud del título
        modelBuilder.Entity<Task>()
        .Property(t => t.Title)
        .HasMaxLength(150)
        .IsRequired();

        // Índice único en Email de Usuario
        modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();
    }
}