using TasksManagement.Domain.Entities;

namespace TasksManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> HasRoleAsync(long code, UserRole role, CancellationToken cancellationToken);
}
