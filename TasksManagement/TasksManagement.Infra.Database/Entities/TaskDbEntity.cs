using TasksManagement.Domain.Enums;

namespace TasksManagement.Infra.Database.Entities;

internal class TaskDbEntity : BaseDbEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime Deadline { get; init; }
    public TaskProgressStatus Status { get; set; }
    public TaskPriority Priority { get; init; }
    public long ProjectId { get; set; }
    public IEnumerable<TaskChangeDbEntity> ChangesHistory { get; set; } = default!;
    public IEnumerable<TaskCommentDbEntity> Comments { get; set; } = default!;

    public ProjectDbEntity Project { get; set; } = default!;
}
