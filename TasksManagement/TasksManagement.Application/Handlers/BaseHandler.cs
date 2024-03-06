using MediatR;
using TasksManagement.Application.Models;
using TasksManagement.Application.Notifications;

namespace TasksManagement.Application.Handlers;

internal abstract class BaseHandler<TCommand, TResult>(IMediator mediator)
    : IRequestHandler<TCommand, Result<TResult>> where TCommand : IRequest<Result<TResult>>
{
    protected readonly IMediator _mediator = mediator;

    public async Task<Result<TResult>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return new Result<TResult>("Task was cancelled");

        try
        {
           return await CommandHandlerAsync(command, cancellationToken);
        }
        catch (Exception exception)
        {
            await _mediator.Publish(new ErrorNotification(exception), cancellationToken);
            return new Result<TResult>(exception.Message);
        }
    }

    protected abstract Task<Result<TResult>> CommandHandlerAsync(TCommand request, CancellationToken cancellationToken);
}
