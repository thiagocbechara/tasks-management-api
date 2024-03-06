using MediatR;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Models;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Handlers.Commands;

internal class UpdateTaskCommandHandler(
    ITaskRepository _repository,
    IMediator mediator)
    : BaseHandler<UpdateTaskCommand, TaskEntity>(mediator)
{
    protected override async Task<Result<TaskEntity>> CommandHandlerAsync(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetAsync(request.Code, cancellationToken);
        if (task is null)
            return new Result<TaskEntity>("Task was not found");

        task.Update(request.Code, request.Name, request.Description, request.Status);
        task = await _repository.UpdateAsync(task, cancellationToken);

        return new Result<TaskEntity>(task);
    }
}
