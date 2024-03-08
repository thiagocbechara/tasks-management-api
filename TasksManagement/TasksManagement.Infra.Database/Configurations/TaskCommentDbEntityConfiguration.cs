using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksManagement.Infra.Database.Entities;
using TasksManagement.Infra.Database.Extensions;

namespace TasksManagement.Infra.Database.Configurations;

internal class TaskCommentDbEntityConfiguration : IEntityTypeConfiguration<TaskCommentDbEntity>
{
    public void Configure(EntityTypeBuilder<TaskCommentDbEntity> builder)
    {
        builder.ConfigureBaseEntity("TaskComments");

        builder.HasOne(e => e.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(e => e.TaskId);

        builder.HasOne(e => e.Author)
            .WithMany(u => u.CommentsMade)
            .HasForeignKey(e => e.AuthorId);
    }
}
