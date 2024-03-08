using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksManagement.Infra.Database.Entities;
using TasksManagement.Infra.Database.Extensions;

namespace TasksManagement.Infra.Database.Configurations;

internal class ProjectDbEntityConfiguration : IEntityTypeConfiguration<ProjectDbEntity>
{
    public void Configure(EntityTypeBuilder<ProjectDbEntity> builder)
    {
        builder.ConfigureBaseEntity("Projects");

        builder.HasOne(e => e.Owner)
            .WithMany(o => o.ProjectsOwned)
            .HasForeignKey(e => e.OwnerId);

        builder.HasMany(e => e.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId);
    }
}
