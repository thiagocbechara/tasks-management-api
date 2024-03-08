using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksManagement.Infra.Database.Entities;
using TasksManagement.Infra.Database.Extensions;

namespace TasksManagement.Infra.Database.Configurations
{
    internal class TaskDbEntityConfiguration : IEntityTypeConfiguration<TaskDbEntity>
    {
        public void Configure(EntityTypeBuilder<TaskDbEntity> builder)
        {
            builder.ConfigureBaseEntity("Tasks");

            builder.HasOne(e => e.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.ProjectId);

            builder.HasMany(e => e.ChangesHistory)
                .WithOne(c => c.Task)
                .HasForeignKey(c => c.TaskId);

            builder.HasMany(e => e.Comments)
                .WithOne(c => c.Task)
                .HasForeignKey(c => c.TaskId);
        }
    }
}
