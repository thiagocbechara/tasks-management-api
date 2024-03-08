using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksManagement.Infra.Database.Entities;

namespace TasksManagement.Infra.Database.Extensions;

internal static class EntityTypeBuilderExtension
{
    public static void ConfigureBaseEntity<TDbEntity>(this EntityTypeBuilder<TDbEntity> builder, string tableName)
        where TDbEntity : BaseDbEntity
    {
        builder.ToTable(tableName);
        builder.HasKey(e => e.Id);
    }
}
