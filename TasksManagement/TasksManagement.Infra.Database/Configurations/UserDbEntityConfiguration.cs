using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksManagement.Infra.Database.Entities;
using TasksManagement.Infra.Database.Extensions;

namespace TasksManagement.Infra.Database.Configurations;

internal class UserDbEntityConfiguration : IEntityTypeConfiguration<UserDbEntity>
{
    public void Configure(EntityTypeBuilder<UserDbEntity> builder)
    {
        builder.ConfigureBaseEntity("Users");

        builder.HasMany(e => e.ProjectsOwned)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId);

        builder.HasMany(e => e.ChangesMade)
            .WithOne(c => c.Author)
            .HasForeignKey(c => c.AuthorId);
    }
}
