using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksManagement.Infra.Database.Entities;
using TasksManagement.Infra.Database.Extensions;

namespace TasksManagement.Infra.Database.Configurations;

internal class TaskChangeDbEntityConfiguration : IEntityTypeConfiguration<TaskChangeDbEntity>
{
    public void Configure(EntityTypeBuilder<TaskChangeDbEntity> builder)
    {
        builder.ConfigureBaseEntity("TaskChanges");

        builder.HasOne(e => e.Task)
            .WithMany(t => t.ChangesHistory)
            .HasForeignKey(e => e.TaskId);

        builder.HasOne(e => e.Author)
            .WithMany(u => u.ChangesMade)
            .HasForeignKey(e => e.AuthorId);
    }
}
