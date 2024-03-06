using MediatR;
using TasksManagement.Application.Models;

namespace TasksManagement.Application.Commands;

public class RemoveTaskCommand : IRequest<Result<bool>>
{
    public required long Code { get; init; }
}
