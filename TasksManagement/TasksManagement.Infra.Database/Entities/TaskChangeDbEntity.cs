namespace TasksManagement.Infra.Database.Entities;

internal class TaskChangeDbEntity : BaseDbEntity
{
    public DateTime When { get; set; }
    public long AuthorId { get; set; }
    public long TaskId { get; set; }
    public string Property { get; set; } = default!;
    public string PreviousValue { get; set; } = default!;
    public string NewValue { get; set; } = default!;

    public UserDbEntity Author { get; set; } = default!;
    public TaskDbEntity Task { get; set; } = default!;
}
