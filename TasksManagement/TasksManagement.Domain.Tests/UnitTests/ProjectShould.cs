using FluentAssertions;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;
using static TasksManagement.Domain.Entities.TaskEntity;

namespace TasksManagement.Domain.Tests.UnitTests;

public class ProjectShould
{
    private readonly TaskEntityBuilder _taskBuilder;
    public ProjectShould()
    {
        _taskBuilder = new TaskEntityBuilder();
    }

    [Fact]
    public void AllowAddNewTasks()
    {
        var project = new ProjectEntity();
        var hasAddedNewTask = project.TryAddTask(CreateTask("Task 1", "Task description 1", TaskPriorityEnum.Low));

        hasAddedNewTask.Should().BeTrue();
        project.Tasks.Should().HaveCount(1);
    }

    [Fact]
    public void NotAllowAddMoreThanTwentyTasks()
    {
        var project = new ProjectEntity();

        var hasAddedNewTask = true;
        for (var i = 0; i <= 20; i++)
        {
            hasAddedNewTask = project.TryAddTask(CreateTask($"Task {i}", $"Task description {i}", TaskPriorityEnum.Low));
        }

        hasAddedNewTask.Should().BeFalse();
        project.Tasks.Should().HaveCount(20);
    }

    [Fact]
    public void IdentifyWhenThereAreUnfinishedTasks()
    {
        var project = new ProjectEntity();
        var taskName = "Task 1";
        var taskDescription = "Task 1";
        var taskPriority = TaskPriorityEnum.Low;
        project.TryAddTask(CreateTask(taskName, taskDescription, taskPriority));

        var hasUnfinishedTasks = project.HasUnfinishedTasks();

        hasUnfinishedTasks.Should().BeTrue();
        project.Tasks.Should().HaveCount(1);
        project.Tasks.Should().AllSatisfy(
            task =>
            {
                task.Name.Should().Be(taskName);
                task.Description.Should().Be(taskDescription);
                task.Priority.Should().Be(taskPriority);
            });
    }


    [Fact]
    public void IdentifyWhenAreAllTasksFinished()
    {
        var project = new ProjectEntity();
        project.TryAddTask(CreateTask("Task 1", "Task description 1", TaskPriorityEnum.Low));
        project.TryAddTask(CreateTask("Task 2", "Task description 2", TaskPriorityEnum.Medium));
        var authorChanges = "Author";
        foreach (var task in project.Tasks)
        {
            task.UpdateStatus(authorChanges, TaskStatusEnum.Done);
        }

        var areAllTasksFinished = project.AreAllTasksFinished();

        areAllTasksFinished.Should().BeTrue();
        project.Tasks.Should().HaveCount(2);
        project.Tasks.Should().AllSatisfy(task =>
        {
            task.Status.Should().Be(TaskStatusEnum.Done);
            task.ChangesHistory.Should().HaveCount(1);
            task.ChangesHistory.Should().AllSatisfy(change => change.Author.Should().Be(authorChanges));
        });
    }

    private TaskEntity CreateTask(
        string taskName,
        string taskDescription,
        TaskPriorityEnum taskPriority,
        long code = 0) =>
        _taskBuilder
        .WithName(taskName)
        .WithDescription(taskDescription)
        .WithPriority(taskPriority)
        .WithCode(code)
        .Build();

}
