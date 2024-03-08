using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TasksManagement.Domain.Repositories;
using TasksManagement.Infra.Database.Contexts;
using TasksManagement.Infra.Database.Entities;

namespace TasksManagement.Infra.Database.Repository;

internal abstract class BaseRepository<TDomainEntity, TDbEntity>(
    ApplicationContext _context,
    IMapper _mapper)
    : IRepository<TDomainEntity>
    where TDbEntity : BaseDbEntity
{
    public async Task<TDomainEntity> AddAsync(TDomainEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        var dbEntity = _mapper.Map<TDbEntity>(entity);

        var entry = await _context
                            .Set<TDbEntity>()
                            .AddAsync(dbEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TDomainEntity>(entry.Entity);
    }

    public Task DeleteAsync(long code, CancellationToken cancellationToken = default) =>
        _context
            .Set<TDbEntity>()
            .Where(e => e.Id == code)
            .ExecuteDeleteAsync(cancellationToken);

    public async Task<IEnumerable<TDomainEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context
                .Set<TDbEntity>()
                .Select(e => _mapper.Map<TDomainEntity>(e))
                .ToListAsync(cancellationToken);

    public async Task<TDomainEntity?> GetAsync(long code, CancellationToken cancellationToken = default)
    {
        var entity = await _context
                        .Set<TDbEntity>()
                        .FirstOrDefaultAsync(e => e.Id == code, cancellationToken);

        if (entity is null)
            return default;

        return _mapper.Map<TDomainEntity>(entity);
    }

    public async Task<TDomainEntity> UpdateAsync(TDomainEntity entity, CancellationToken cancellationToken = default)
    {
        var dbEntity = _mapper.Map<TDbEntity>(entity);

        var entry = _context
                        .Set<TDbEntity>()
                        .Update(dbEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TDomainEntity>(entry.Entity);
    }
}
