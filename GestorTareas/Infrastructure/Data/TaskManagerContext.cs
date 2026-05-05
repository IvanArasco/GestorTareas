using GestorDeTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = GestorDeTareas.Domain.Entities.Task;

public class TaskManagerContext : DbContext
{
    // Cada DbSet representa una tabla en la BD
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
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