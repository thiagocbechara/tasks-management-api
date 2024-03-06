using FluentAssertions;
using MediatR;
using Moq;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Handlers.Commands;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Tests.UnitTests;

public class UpdateTaskCommandHandlerShould
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UpdateTaskCommandHandler _handler;

    public UpdateTaskCommandHandlerShould()
    {
        _repositoryMock = new Mock<ITaskRepository>(MockBehavior.Strict);
        _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
        _handler = new UpdateTaskCommandHandler(
            _repositoryMock.Object,
            _mediatorMock.Object);
    }

    [Fact]
    public async Task NotUpdateWhenNotFound()
    {
        var command = new UpdateTaskCommand(
            code: 1,
            name: "New Task Name",
            description: "Description",
            status: TaskProgressStatus.Done,
            authorCode: 1);
        _repositoryMock.Setup(r => r.GetAsync(command.Code, CancellationToken.None))
            .ReturnsAsync((TaskEntity?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeFalse();
        result.ErrorMessage.Should().Be("Task was not found");
        _repositoryMock.Verify(r => r.GetAsync(command.Code, CancellationToken.None), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<TaskEntity>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task UpdateSuccessful()
    {
        var command = new UpdateTaskCommand(
            code: 1,
            name: "New Task Name",
            description: "Description",
            status: TaskProgressStatus.Done,
            authorCode: 1);
        _repositoryMock.Setup(r => r.GetAsync(command.Code, CancellationToken.None))
            .ReturnsAsync(new TaskEntity
            {
                Description = command.Description,
            });

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<TaskEntity>(), CancellationToken.None))
            .ReturnsAsync((TaskEntity t, CancellationToken _) => t);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Name.Should().Be(command.Name);
        result.Data.Description.Should().Be(command.Description);
        result.Data.Status.Should().Be(command.Status);
        result.Data.ChangesHistory.Should().HaveCount(2);
        result.Data.ChangesHistory.Should().AllSatisfy(
            c => c.Author.Code.Should().Be(command.AuthorCode));
        result.Data.ChangesHistory.Should().Satisfy(
            c => c.Property == nameof(TaskEntity.Name) && c.NewValue == command.Name,
            c => c.Property == nameof(TaskEntity.Status) && c.NewValue == command.Status.ToString());

        _repositoryMock.Verify(r => r.GetAsync(command.Code, CancellationToken.None), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<TaskEntity>(), CancellationToken.None), Times.Once);
    }
}
