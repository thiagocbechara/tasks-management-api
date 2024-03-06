using MediatR;
using TasksManagement.Application.Models;

namespace TasksManagement.Application.Commands;

public class RemoveProjectCommand : IRequest<Result<bool>>
{
    public required long Code { get; init; }
}
