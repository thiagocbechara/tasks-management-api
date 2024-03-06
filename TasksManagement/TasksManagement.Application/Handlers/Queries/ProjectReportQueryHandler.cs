using MediatR;
using TasksManagement.Application.Models;
using TasksManagement.Application.Queries;
using TasksManagement.Application.ViewModels;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;
using TasksManagement.Domain.Repositories;

namespace TasksManagement.Application.Handlers.Queries;

internal class ProjectReportQueryHandler(
    IUserRepository _userRepository,
    IProjectRepository _projectRepository,
    IMediator mediator)
    : BaseHandler<ProjectReportQuery, ProjectReportModel>(mediator)
{
    protected override async Task<Result<ProjectReportModel>> CommandHandlerAsync(ProjectReportQuery request, CancellationToken cancellationToken)
    {
        var isManager = await IsManager(request, cancellationToken);
        if (!isManager)
            return new Result<ProjectReportModel>("Only manager users can generate project reports");

        var project = await _projectRepository.GetAsync(request.ProjectCode, cancellationToken);

        if (project is null)
            return new Result<ProjectReportModel>("Project was not found");

        var usersInProject = project.Tasks.SelectMany(t => t.ChangesHistory.Select(c => c.Author)).DistinctBy(u => u.Code);
        var userTaskReport = usersInProject.Select(user => new TaskUserReportModel
        {
            UserCode = user.Code,
            UserName = user.Name,
            DoingTasks = GetTasksTypeByUser(user, project.Tasks, TaskProgressStatus.Doing),
            DoneTasks = GetTasksTypeByUser(user, project.Tasks, TaskProgressStatus.Done),
            TotalComments = project.Tasks.Sum(task => task.Comments.Count(comment => comment.Author.Code == user.Code))
        });
        var report = new ProjectReportModel
        {
            ProjectCode = project.Code,
            ProjectName = project.Name,
            PendingTasks = project.Tasks.Count(task => task.Status == TaskProgressStatus.Pending),
            DoingTasks = project.Tasks.Count(task => task.Status == TaskProgressStatus.Doing),
            DoneTasks = project.Tasks.Count(task => task.Status == TaskProgressStatus.Done),
            TasksPerUser = userTaskReport
        };

        return new Result<ProjectReportModel>(report);
    }

    private static int GetTasksTypeByUser(UserEntity user, IList<TaskEntity> tasks, TaskProgressStatus status) =>
        tasks.Count(task =>
            task.Status == status &&
            task.ChangesHistory.Any(change =>
                change.Author.Code == user.Code &&
                change.Property == nameof(TaskEntity.Status) &&
                change.NewValue == status.ToString()));

    private Task<bool> IsManager(ProjectReportQuery request, CancellationToken cancellationToken) =>
        _userRepository.HasRoleAsync(request.RequesterUserCode, UserRole.Manager, cancellationToken);
}
