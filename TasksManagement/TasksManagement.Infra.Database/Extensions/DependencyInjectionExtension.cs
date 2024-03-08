using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TasksManagement.Domain.Repositories;
using TasksManagement.Infra.Database.Contexts;
using TasksManagement.Infra.Database.Profiles;
using TasksManagement.Infra.Database.Repository;

namespace TasksManagement.Infra.Database.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDatabaseDependecies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(opt =>
            opt.UseSqlServer(configuration["DbConnectionString"]));

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddAutoMapper(typeof(DatabaseProfile));

        return services;
    }
}
