using MediatR;
using TasksManagement.Application.Models;
using TasksManagement.Domain.Entities;

namespace TasksManagement.Application.Commands;

public class UpdateTaskCommand(
    long code,
    string name,
    string description,
    Domain.Enums.TaskProgressStatus status,
    long authorCode)
    : IRequest<Result<TaskEntity>>
{
    public long Code { get; } = code;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public Domain.Enums.TaskProgressStatus Status { get; } = status;
    public long AuthorCode { get; } = authorCode;
}
