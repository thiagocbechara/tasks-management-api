using TasksManagement.Domain.Enums;

namespace TasksManagement.Domain.Entities;

public class ProjectEntity
{
    public ProjectEntity()
    {
        Tasks = [];
    }

    public long Code { get; set; }
    public string Name { get; set; } = default!;
    public UserEntity Owner { get; set; } = default!;
    public IList<TaskEntity> Tasks { get; set; }

    public bool TryAddTask(TaskEntity? task)
    {
        ArgumentNullException.ThrowIfNull(task);

        if (Tasks.Count == 20)
            return false;

        Tasks.Add(task);
        return true;
    }

    public bool HasUnfinishedTasks() =>
        Tasks.Any(task => task.Status != TaskStatusEnum.Done);

    public bool AreAllTasksFinished() =>
        Tasks.All(task => task.Status == TaskStatusEnum.Done);

    public bool RemoveTask(int code)
    {
        var task = Tasks.FirstOrDefault(task => task.Code == code);
        if (task is null)
            return false;

        Tasks.Remove(task);
        return true;
    }
}
