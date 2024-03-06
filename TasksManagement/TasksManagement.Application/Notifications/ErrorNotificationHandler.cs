using MediatR;
using Microsoft.Extensions.Logging;

namespace TasksManagement.Application.Notifications;

internal class ErrorNotificationHandler(ILogger<ErrorNotificationHandler> _logger)
    : INotificationHandler<ErrorNotification>
{
    public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.CompletedTask;
        }

        _logger.LogError("{ErrorMessage}{NewLine}Stacketrace: {StackTrace}",
            notification.ErrorMessage,
            Environment.NewLine,
            notification.StackTrace);
        return Task.CompletedTask;
    }
}
