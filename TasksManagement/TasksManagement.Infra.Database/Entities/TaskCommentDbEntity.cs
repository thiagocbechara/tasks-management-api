namespace TasksManagement.Infra.Database.Entities;

internal class TaskCommentDbEntity : BaseDbEntity
{
    public long AuthorId { get; set; }
    public long TaskId { get; set; }
    public string Comment { get; set; } = default!;

    public UserDbEntity Author { get; set; } = default!;
    public TaskDbEntity Task { get; set; } = default!;
}