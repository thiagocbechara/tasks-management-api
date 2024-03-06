using MediatR;
using TasksManagement.Application.Models;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;

namespace TasksManagement.Application.Commands;

public class CreateTaskCommand(
    long projectCode,
    string name,
    string description,
    DateTime deadline,
    TaskPriority priority)
    : IRequest<Result<TaskEntity>>
{
    public long ProjectCode { get; } = projectCode;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public DateTime Deadline { get; } = deadline;
    public TaskPriority Priority { get; } = priority;
}
