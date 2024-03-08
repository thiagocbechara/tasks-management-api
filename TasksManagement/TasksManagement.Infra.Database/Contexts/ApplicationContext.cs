using Microsoft.EntityFrameworkCore;
using TasksManagement.Domain.Enums;
using TasksManagement.Infra.Database.Configurations;
using TasksManagement.Infra.Database.Entities;

namespace TasksManagement.Infra.Database.Contexts;

internal class ApplicationContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ProjectDbEntity> Projects { get; set; }
    public DbSet<TaskDbEntity> Tasks { get; set; }
    public DbSet<UserDbEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectDbEntityConfiguration).Assembly);

        modelBuilder.Entity<UserDbEntity>()
            .HasData(
                new UserDbEntity { Id = 1, Name = "Logan", Role = UserRole.Manager },
                new UserDbEntity { Id = 2, Name = "Scott", Role = UserRole.Regular },
                new UserDbEntity { Id = 3, Name = "Jean", Role = UserRole.Regular }
            );
    }
}
