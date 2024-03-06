using FluentAssertions;
using MediatR;
using Moq;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Handlers.Commands;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Tests.UnitTests;

public class RemoveProjectCommandHandlerShould
{
    private readonly Mock<IProjectRepository> _repositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly RemoveProjectCommandHandler _handler;

    public RemoveProjectCommandHandlerShould()
    {
        _repositoryMock = new Mock<IProjectRepository>(MockBehavior.Strict);
        _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
        _handler = new RemoveProjectCommandHandler(
            _repositoryMock.Object,
            _mediatorMock.Object);
    }

    [Fact]
    public async Task NotRemoveWhenProjectNotFound()
    {
        var command = new RemoveProjectCommand { Code = 1 };
        _repositoryMock.Setup(r => r.GetAsync(command.Code, CancellationToken.None))
            .ReturnsAsync((ProjectEntity?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeFalse();
        result.ErrorMessage.Should().Be("Project was not found");
        _repositoryMock.Verify(r => r.GetAsync(command.Code, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task NotRemoveWhenProjectHasUnfinishedTasks()
    {
        var command = new RemoveProjectCommand { Code = 1 };
        _repositoryMock.Setup(r => r.GetAsync(command.Code, CancellationToken.None))
            .ReturnsAsync(new ProjectEntity
            {
                Tasks = [new TaskEntity { Status = TaskProgressStatus.Doing }]
            });

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeFalse();
        result.ErrorMessage.Should().Be("Project cannot be removed because has unfinished tasks");
        _repositoryMock.Verify(r => r.GetAsync(command.Code, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task RemoveProjectSuccessful()
    {
        var command = new RemoveProjectCommand { Code = 1 };
        _repositoryMock.Setup(r => r.GetAsync(command.Code, CancellationToken.None))
            .ReturnsAsync(new ProjectEntity());

        _repositoryMock.Setup(r => r.DeleteAsync(command.Code, CancellationToken.None))
            .ReturnsAsync(new ProjectEntity());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeTrue();
        result.Data.Should().BeTrue();
        _repositoryMock.Verify(r => r.GetAsync(command.Code, CancellationToken.None), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(command.Code, CancellationToken.None), Times.Once);
    }
}
