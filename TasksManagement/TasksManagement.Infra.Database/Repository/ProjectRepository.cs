using AutoMapper;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Repositories;
using TasksManagement.Infra.Database.Contexts;
using TasksManagement.Infra.Database.Entities;

namespace TasksManagement.Infra.Database.Repository;

internal class ProjectRepository(
    ApplicationContext context,
    IMapper mapper)
        : BaseRepository<ProjectEntity, ProjectDbEntity>(context, mapper), IProjectRepository
{
}
