using TasksManagement.Domain.Enums;

namespace TasksManagement.Domain.Entities;

public class TaskEntity
{
    public TaskEntity()
    {
        ChangesHistory = [];
        Comments = [];
        Status = TaskProgressStatus.Pending;
    }

    public long Code { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime Deadline { get; init; }
    public TaskProgressStatus Status { get; set; }
    public TaskPriority Priority { get; init; }
    public IList<TaskChangeEntity> ChangesHistory { get; set; }
    public IList<TaskCommentEntity> Comments { get; set; }

    public void Update(long authorCode, string newName, string newDescription)
    {
        UpdateName(authorCode, newName);
        UpdateDescription(authorCode, newDescription);
    }

    public void Update(long authorCode, string newName, string newDescription, Enums.TaskProgressStatus newStatus)
    {
        Update(authorCode, newName, newDescription);
        UpdateStatus(authorCode, newStatus);
    }

    private void UpdateName(long authorCode, string newName)
    {
        if (string.IsNullOrWhiteSpace(newName) || Name == newName)
            return;
        AddChangeHistory(authorCode, nameof(Name), Name, newName);
        Name = newName;
    }

    private void UpdateDescription(long authorCode, string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription) || Description == newDescription)
            return;
        AddChangeHistory(authorCode, nameof(Description), Description, newDescription);
        Description = newDescription;
    }

    public void UpdateStatus(long authorCode, Enums.TaskProgressStatus newStatus)
    {
        if (Status == newStatus)
            return;
        AddChangeHistory(authorCode, nameof(Status), Status.ToString(), newStatus.ToString());
        Status = newStatus;
    }

    public void AddComment(long authorCode, string comment)
    {
        Comments.Add(new TaskCommentEntity
        {
            Author = new() { Code = authorCode },
            Comment = comment
        });

        AddChangeHistory(authorCode, nameof(Comments), string.Empty, comment);
    }

    private void AddChangeHistory(long authorCode, string property, string previousValue, string newValue)
    {
        ChangesHistory.Add(new()
        {
            Author = new() { Code = authorCode },
            When = DateTime.Now,
            Property = property,
            PreviousValue = previousValue,
            NewValue = newValue
        });
    }
}
