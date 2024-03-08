namespace TasksManagement.Infra.Database.Entities;

internal class ProjectDbEntity : BaseDbEntity
{
    public string Name { get; set; } = default!;
    public long OwnerId { get; set; }
    public IEnumerable<TaskDbEntity> Tasks { get; set; } = default!;

    public UserDbEntity Owner { get; set; } = default!;
}
