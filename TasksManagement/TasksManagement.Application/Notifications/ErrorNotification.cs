using MediatR;

namespace TasksManagement.Application.Notifications;

internal class ErrorNotification(Exception exception) : INotification
{
    public string ErrorMessage { get; } = exception.Message;
    public string? StackTrace { get; } = exception.StackTrace;
}
