using AutoMapper;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Repositories;
using TasksManagement.Infra.Database.Contexts;
using TasksManagement.Infra.Database.Entities;

namespace TasksManagement.Infra.Database.Repository;

internal class TaskRepository(
    ApplicationContext context,
    IMapper mapper)
    : BaseRepository<TaskEntity, TaskDbEntity>(context, mapper), ITaskRepository
{
}
