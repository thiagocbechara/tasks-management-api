namespace TasksManagement.Domain.Entities;

public class TaskChangeEntity
{
    public long Code { get; set; }
    public DateTime When { get; set; }
    public string Author { get; set; } = default!;
    public string Property { get; set; } = default!;
    public string PreviousValue { get; set; } = default!;
    public string NewValue { get; set; } = default!;
}
