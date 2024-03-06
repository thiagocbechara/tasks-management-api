using Microsoft.Extensions.DependencyInjection;
using TasksManagement.Application.Commands;

namespace TasksManagement.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationDependecies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateProjectCommand>());
        return services;
    }
}
