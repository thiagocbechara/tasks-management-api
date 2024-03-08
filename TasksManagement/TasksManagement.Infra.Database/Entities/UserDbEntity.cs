using TasksManagement.Domain.Enums;

namespace TasksManagement.Infra.Database.Entities;

internal class UserDbEntity : BaseDbEntity
{
    public string Name { get; set; } = default!;
    public UserRole Role { get; set; }

    public IEnumerable<ProjectDbEntity> ProjectsOwned { get; set; } = default!;
    public IEnumerable<TaskChangeDbEntity> ChangesMade { get; set; } = default!;
    public IEnumerable<TaskCommentDbEntity> CommentsMade { get; set; } = default!;
}
