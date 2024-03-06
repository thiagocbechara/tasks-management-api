namespace TasksManagement.Application.ViewModels;

public class ProjectReportModel
{
    public long ProjectCode { get; set; }
    public string ProjectName { get; set; } = default!;
    public int PendingTasks { get; set; }
    public int DoingTasks { get; set; }
    public int DoneTasks { get; set; }
    public int TotalTasks => PendingTasks + DoingTasks + DoneTasks;
    public int UfinishedTasks => PendingTasks + DoingTasks;
    public IEnumerable<TaskUserReportModel> TasksPerUser { get; set; } = default!;
}
