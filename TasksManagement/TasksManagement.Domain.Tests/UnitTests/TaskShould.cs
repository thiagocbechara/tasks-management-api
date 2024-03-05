using FluentAssertions;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;
using static TasksManagement.Domain.Entities.TaskEntity;

namespace TasksManagement.Domain.Tests.UnitTests;

public class TaskShould
{
    private readonly TaskEntity _task;

    public TaskShould()
    {
        _task = new TaskEntityBuilder()
            .WithName("Task 1")
            .WithDescription("Description 1")
            .WithPriority(TaskPriorityEnum.Low)
            .Build();
    }

    [Fact]
    public void RegisterChangeHistoryWhenIsChanged()
    {
        var changesAuthor = "Author";
        var previousName = _task.Name;
        var previousDescription = _task.Description;
        var newName = "Task 1.1";
        var newDescription = "Description 1.1";

        _task.Update(changesAuthor, newName, newDescription);

        _task.ChangesHistory.Should().HaveCount(2);
        _task.ChangesHistory
            .Should()
            .AllSatisfy(change =>
            {
                change.Author.Should().Be(changesAuthor);
                change.When.Date.Should().Be(DateTime.Today);
            })
            .And
            .Satisfy(
            change => change.Property == nameof(_task.Name)
                        && change.PreviousValue == previousName
                        && change.NewValue == newName,

            change => change.Property == nameof(_task.Description)
                        && change.PreviousValue == previousDescription
                        && change.NewValue == newDescription
            );
    }

    [Fact]
    public void RegisterChangeHistoryWhenCommentIsAdded()
    {
        var author = "Author";
        var comment = "A nice comment about this task!";

        _task.AddComment(author, comment);

        _task.Comments.Should().HaveCount(1);
        _task.Comments.Should()
            .AllSatisfy(taskComment =>
            {
                taskComment.Author.Should().Be(author);
                taskComment.Comment.Should().Be(comment);
            });

        _task.ChangesHistory.Should().HaveCount(1);
        _task.ChangesHistory.Should().AllSatisfy(
            change =>
            {
                change.Author.Should().Be(author);
                change.When.Date.Should().Be(DateTime.Today);
                change.Property.Should().Be(nameof(_task.Comments));
                change.PreviousValue.Should().Be(string.Empty);
            });
    }
}