using MediatR;
using TasksManagement.Application.Models;
using TasksManagement.Domain.Entities;

namespace TasksManagement.Application.Commands;

public class CreateProjectCommand(string name, long ownerCode)
        : IRequest<Result<ProjectEntity>>
{
    public string Name { get; } = name;
    public long OwnerCode { get; } = ownerCode;
}
