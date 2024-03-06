using FluentAssertions;
using MediatR;
using Moq;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Handlers.Commands;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Tests.UnitTests
{
    public class CreateProjectCommandHandlerShould
    {
        [Fact]
        public async Task CreateNewProjectSuccessful()
        {
            var repositoryMock = new Mock<IProjectRepository>(MockBehavior.Strict);
            var mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            var handler = new CreateProjectCommandHandler(
                repositoryMock.Object,
                mediatorMock.Object);
            var command = new CreateProjectCommand("Project", 1);
            var cancellationToken = CancellationToken.None;
            var projectCode = 1;

            repositoryMock.Setup(r => r.AddAsync(It.IsAny<ProjectEntity>(), cancellationToken))
                .Callback((ProjectEntity project, CancellationToken _) => project.Code = projectCode)
                .ReturnsAsync((ProjectEntity project, CancellationToken _) => project);

            var result = await handler.Handle(command, cancellationToken);

            result.WasSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Code.Should().Be(projectCode);
            result.Data.Name.Should().Be(command.Name);
            result.Data.Owner.Code.Should().Be(command.OwnerCode);

            repositoryMock.Verify(r => r.AddAsync(It.IsAny<ProjectEntity>(), cancellationToken), Times.Once);
        }
    }
}