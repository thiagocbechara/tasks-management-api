using MediatR;
using TasksManagement.Application.Models;
using TasksManagement.Application.ViewModels;

namespace TasksManagement.Application.Queries;

public class ProjectReportQuery(
    long requesterUserCode,
    long projectCode)
    : IRequest<Result<ProjectReportModel>>
{
    public long RequesterUserCode { get; } = requesterUserCode;
    public long ProjectCode { get; } = projectCode;
}
