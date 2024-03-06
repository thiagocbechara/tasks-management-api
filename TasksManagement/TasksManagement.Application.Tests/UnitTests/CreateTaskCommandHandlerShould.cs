using FluentAssertions;
using MediatR;
using Moq;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Handlers.Commands;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Tests.UnitTests;

public class CreateTaskCommandHandlerShould
{
    private readonly Mock<IProjectRepository> _repositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CreateTaskCommandHandler _handler;

    public CreateTaskCommandHandlerShould()
    {
        _repositoryMock = new Mock<IProjectRepository>(MockBehavior.Strict);
        _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
        _handler = new CreateTaskCommandHandler(
            _repositoryMock.Object,
            _mediatorMock.Object);
    }

    [Fact]
    public async Task CreateNewTaskSuccessful()
    {
        var command = new CreateTaskCommand(
            projectCode: 1,
            name: "Task 1",
            description: "Task description 1",
            deadline: DateTime.Today.AddDays(1),
            priority: TaskPriority.Medium);

        _repositoryMock.Setup(r => r.GetAsync(command.ProjectCode, CancellationToken.None))
            .ReturnsAsync(new ProjectEntity());

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ProjectEntity>(), CancellationToken.None))
            .Callback((ProjectEntity project, CancellationToken _) => project.Tasks.First().Code = 1)
            .ReturnsAsync((ProjectEntity project, CancellationToken _) => project);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Name.Should().Be(command.Name);
        result.Data.Code.Should().BeGreaterThan(0);
        result.Data.Description.Should().Be(command.Description);
        result.Data.Deadline.Should().Be(command.Deadline);
        result.Data.Priority.Should().Be(command.Priority);
        result.Data.Status.Should().Be(TaskProgressStatus.Pending);

        _repositoryMock.Verify(r => r.GetAsync(command.ProjectCode, CancellationToken.None), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ProjectEntity>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task NotAllowedCreateNewTaskWhenProjectNotFound()
    {
        var command = new CreateTaskCommand(
            projectCode: 1,
            name: "Task 1",
            description: "Task description 1",
            deadline: DateTime.Today.AddDays(1),
            priority: TaskPriority.Medium);

        _repositoryMock.Setup(r => r.GetAsync(command.ProjectCode, CancellationToken.None))
            .ReturnsAsync((ProjectEntity?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeFalse();
        result.ErrorMessage.Should().Be("Project was not found");
        _repositoryMock.Verify(r => r.GetAsync(command.ProjectCode, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task NotAllowedCreateNewTaskWhenProjectHasTwentyTasks()
    {
        var command = new CreateTaskCommand(
            projectCode: 1,
            name: "Task 1",
            description: "Task description 1",
            deadline: DateTime.Today.AddDays(1),
            priority: TaskPriority.Medium);

        _repositoryMock.Setup(r => r.GetAsync(command.ProjectCode, CancellationToken.None))
            .ReturnsAsync(new ProjectEntity
            {
                Tasks = Enumerable.Repeat<TaskEntity>(default!, 20).ToList()
            });

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeFalse();
        result.ErrorMessage.Should().Be("Cannot add more than 20 tasks on a project");
        _repositoryMock.Verify(r => r.GetAsync(command.ProjectCode, CancellationToken.None), Times.Once);
    }
}
