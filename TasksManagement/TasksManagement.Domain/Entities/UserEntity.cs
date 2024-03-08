using TasksManagement.Domain.Enums;

namespace TasksManagement.Domain.Entities;

public class UserEntity
{
    public long Code { get; set; }
    public string Name { get; set; } = default!;
    public UserRole Role { get; set; }
}
