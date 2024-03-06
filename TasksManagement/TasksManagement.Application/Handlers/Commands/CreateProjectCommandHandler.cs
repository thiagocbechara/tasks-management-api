using MediatR;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Models;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Handlers.Commands;

internal class CreateProjectCommandHandler(
    IProjectRepository _repository,
    IMediator mediator)
    : BaseHandler<CreateProjectCommand, ProjectEntity>(mediator)
{
    protected override async Task<Result<ProjectEntity>> CommandHandlerAsync(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = new ProjectEntity
        {
            Name = command.Name,
            Owner = new UserEntity
            {
                Code = command.OwnerCode
            }
        };

        project = await _repository.AddAsync(project, cancellationToken);
        return new Result<ProjectEntity>(project);
    }
}
