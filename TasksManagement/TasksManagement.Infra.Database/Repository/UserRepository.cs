using Microsoft.EntityFrameworkCore;
using TasksManagement.Domain.Enums;
using TasksManagement.Domain.Repositories;
using TasksManagement.Infra.Database.Contexts;

namespace TasksManagement.Infra.Database.Repository;

internal class UserRepository(ApplicationContext _context)
    : IUserRepository
{
    public Task<bool> HasRoleAsync(long code, UserRole role, CancellationToken cancellationToken) =>
        _context.Users
            .AnyAsync(u => u.Id == code && u.Role == role, cancellationToken);
}
