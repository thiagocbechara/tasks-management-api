using MediatR;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Models;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Handlers.Commands;

internal class CreateTaskCommandHandler(
    IProjectRepository _projectRepository,
    IMediator mediator)
    : BaseHandler<CreateTaskCommand, TaskEntity>(mediator)
{
    protected override async Task<Result<TaskEntity>> CommandHandlerAsync(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var task = new TaskEntity
        {
            Name = command.Name,
            Description = command.Description,
            Deadline = command.Deadline,
            Priority = command.Priority,
        };

        var project = await _projectRepository.GetAsync(command.ProjectCode, cancellationToken);
        if (project is null)
            return new Result<TaskEntity>("Project was not found");

        if (project.TryAddTask(task))
        {
            project = await _projectRepository.UpdateAsync(project, cancellationToken);
            return new Result<TaskEntity>(project.Tasks.Last());
        }
        return new Result<TaskEntity>("Cannot add more than 20 tasks on a project");
    }
}
