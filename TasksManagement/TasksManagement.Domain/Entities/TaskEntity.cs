using TasksManagement.Domain.Enums;

namespace TasksManagement.Domain.Entities;

public partial class TaskEntity
{
    private TaskEntity()
    {
        ChangesHistory = [];
        Comments = [];
    }

    public long Code { get; private set; }
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public DateTime Deadline { get; set; }
    public TaskStatusEnum Status { get; private set; }
    public TaskPriorityEnum Priority { get; private set; }
    public IList<TaskChangeEntity> ChangesHistory { get; private set; }
    public IList<TaskCommentEntity> Comments { get; private set; }

    public void Update(string author, string newName, string newDescription)
    {
        UpdateName(author, newName);
        UpdateDescription(author, newDescription);
    }

    public void Update(string author, string newName, string newDescription, TaskStatusEnum newStatus)
    {
        Update(author, newName, newDescription);
        UpdateStatus(author, newStatus);
    }

    private void UpdateName(string author, string newName)
    {
        if (string.IsNullOrWhiteSpace(newName) || Name == newName)
            return;
        AddChangeHistory(author, nameof(Name), Name, newName);
        Name = newName;
    }

    private void UpdateDescription(string author, string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription) || Description == newDescription)
            return;
        AddChangeHistory(author, nameof(Description), Description, newDescription);
        Description = newDescription;
    }

    public void UpdateStatus(string author, TaskStatusEnum newStatus)
    {
        if (Status == newStatus)
            return;
        AddChangeHistory(author, nameof(Status), Status.ToString(), newStatus.ToString());
        Status = newStatus;
    }

    public void AddComment(string author, string comment)
    {
        Comments.Add(new TaskCommentEntity
        {
            Author = author,
            Comment = comment
        });

        AddChangeHistory(author, nameof(Comments), string.Empty, comment);
    }

    private void AddChangeHistory(string author, string property, string previousValue, string newValue)
    {
        ChangesHistory.Add(new()
        {
            Author = author,
            When = DateTime.Now,
            Property = property,
            PreviousValue = previousValue,
            NewValue = newValue
        });
    }
}
