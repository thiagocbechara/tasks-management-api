using FluentAssertions;
using MediatR;
using Moq;
using TasksManagement.Application.Handlers.Queries;
using TasksManagement.Application.Queries;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Tests.UnitTests;

public class ProjectReportQueryHandlerShould
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IProjectRepository> _projectRepository;
    private readonly Mock<IMediator> _mediator;
    private readonly ProjectReportQueryHandler _handler;

    public ProjectReportQueryHandlerShould()
    {
        _userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
        _projectRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
        _mediator = new Mock<IMediator>(MockBehavior.Strict);
        _handler = new ProjectReportQueryHandler(
            _userRepository.Object,
            _projectRepository.Object,
            _mediator.Object);
    }

    [Fact]
    public async Task NotGenerateWhenUserIsNotManager()
    {
        var command = new ProjectReportQuery(1, 1);
        _userRepository.Setup(r => r.HasRoleAsync(command.RequesterUserCode, UserRole.Manager, CancellationToken.None))
            .ReturnsAsync(false);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeFalse();
        result.ErrorMessage.Should().Be("Only manager users can generate project reports");

        _userRepository.Verify(r => r.HasRoleAsync(command.RequesterUserCode, UserRole.Manager, CancellationToken.None), Times.Once);
        _projectRepository.Verify(r => r.GetAsync(command.ProjectCode, CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task NotGenerateWhenProjectNotFound()
    {
        var command = new ProjectReportQuery(1, 1);
        _userRepository.Setup(r => r.HasRoleAsync(command.RequesterUserCode, UserRole.Manager, CancellationToken.None))
            .ReturnsAsync(true);

        _projectRepository.Setup(r => r.GetAsync(command.ProjectCode, CancellationToken.None))
            .ReturnsAsync((ProjectEntity?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeFalse();
        result.ErrorMessage.Should().Be("Project was not found");

        _userRepository.Verify(r => r.HasRoleAsync(command.RequesterUserCode, UserRole.Manager, CancellationToken.None), Times.Once);
        _projectRepository.Verify(r => r.GetAsync(command.ProjectCode, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GenerateSuccessful()
    {
        var command = new ProjectReportQuery(1, 1);
        _userRepository.Setup(r => r.HasRoleAsync(1, UserRole.Manager, CancellationToken.None))
            .ReturnsAsync(true);

        var project = new ProjectEntity
        {
            Code = command.ProjectCode,
            Name = "Project name",
            Tasks = [
                        new TaskEntity
                        {
                            Status = TaskProgressStatus.Pending,
                        },
                        new TaskEntity
                        {
                            Status = TaskProgressStatus.Doing,
                            ChangesHistory = [
                                new TaskChangeEntity
                                {
                                    Author = new UserEntity{ Code = 1, Name = "User 1" },
                                    Property = nameof(TaskEntity.Status),
                                    NewValue = nameof(TaskProgressStatus.Doing)
                                }
                                ]
                        },
                        new TaskEntity
                        {
                            Status = TaskProgressStatus.Doing,
                            ChangesHistory = [
                                new TaskChangeEntity
                                {
                                    Author = new UserEntity{ Code = 2, Name = "User 2" },
                                    Property = nameof(TaskEntity.Status),
                                    NewValue = nameof(TaskProgressStatus.Doing)
                                }
                                ]
                        },
                        new TaskEntity
                        {
                            Status = TaskProgressStatus.Done,
                            ChangesHistory = [
                                new TaskChangeEntity
                                {
                                    Author = new UserEntity{ Code = 1, Name = "User 1" },
                                    Property = nameof(TaskEntity.Status),
                                    NewValue = nameof(TaskProgressStatus.Done)
                                }
                                ]
                        }
                    ]
        };
        _projectRepository.Setup(r => r.GetAsync(command.ProjectCode, CancellationToken.None))
            .ReturnsAsync(project);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.WasSuccessful.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.ProjectCode.Should().Be(project.Code);
        result.Data.ProjectName.Should().Be(project.Name);
        result.Data.PendingTasks.Should().Be(1);
        result.Data.DoingTasks.Should().Be(2);
        result.Data.DoneTasks.Should().Be(1);
        result.Data.TasksPerUser.Should().HaveCount(2);
        result.Data.TasksPerUser.Should().Satisfy(
            t => t.UserCode == 1 &&
                 t.UserName == "User 1" &&
                 t.DoingTasks == 1 &&
                 t.DoneTasks == 1 &&
                 t.TotalComments == 0,
            t => t.UserCode == 2 &&
                 t.UserName == "User 2" &&
                 t.DoingTasks == 1 &&
                 t.DoneTasks == 0 &&
                 t.TotalComments == 0
            );

        _userRepository.Verify(r => r.HasRoleAsync(command.RequesterUserCode, UserRole.Manager, CancellationToken.None), Times.Once);
        _projectRepository.Verify(r => r.GetAsync(command.ProjectCode, CancellationToken.None), Times.Once);
    }
}
