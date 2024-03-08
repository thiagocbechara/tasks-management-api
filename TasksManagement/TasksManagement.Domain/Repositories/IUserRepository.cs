using TasksManagement.Domain.Enums;

namespace TasksManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> HasRoleAsync(long code, UserRole role, CancellationToken cancellationToken);
}
