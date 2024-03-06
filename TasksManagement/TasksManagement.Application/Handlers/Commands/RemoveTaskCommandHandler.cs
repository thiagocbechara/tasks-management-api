using MediatR;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Models;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Handlers.Commands;

internal class RemoveTaskCommandHandler(
    ITaskRepository _repository,
    IMediator mediator)
    : BaseHandler<RemoveTaskCommand, bool>(mediator)
{
    protected override async Task<Result<bool>> CommandHandlerAsync(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Code, cancellationToken);
        return new Result<bool>(true);
    }
}
