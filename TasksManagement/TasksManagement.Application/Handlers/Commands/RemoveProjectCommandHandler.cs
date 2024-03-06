using MediatR;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Models;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Handlers.Commands;

internal class RemoveProjectCommandHandler(
    IProjectRepository _repository,
    IMediator mediator)
    : BaseHandler<RemoveProjectCommand, bool>(mediator)
{
    protected override async Task<Result<bool>> CommandHandlerAsync(RemoveProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _repository.GetAsync(command.Code, cancellationToken);
        if (project is null)
            return new Result<bool>("Project was not found");

        if (project.HasUnfinishedTasks())
            return new Result<bool>("Project cannot be removed because has unfinished tasks");

        await _repository.DeleteAsync(command.Code, cancellationToken);
        return new Result<bool>(true);
    }
}
