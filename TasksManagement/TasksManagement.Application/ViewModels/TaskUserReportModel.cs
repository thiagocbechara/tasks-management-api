namespace TasksManagement.Application.ViewModels;

public class TaskUserReportModel
{
    public long UserCode { get; set; }
    public string UserName { get; set; } = default!;
    public int DoingTasks { get; set; }
    public int DoneTasks { get; set; }
    public int TotalComments { get; set; }
    public int TotalTasks => DoingTasks + DoneTasks;
}