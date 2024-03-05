namespace TasksManagement.Domain.Entities;

public class TaskCommentEntity
{
    public long Code { get; set; }
    public UserEntity Author { get; set; } = default!;
    public string Comment { get; set; } = default!;
}