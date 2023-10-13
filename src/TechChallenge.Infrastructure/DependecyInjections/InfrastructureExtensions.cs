using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Domain.Repositories;
using TechChallenge.Infrastructure.Repositories;

namespace TechChallenge.Infrastructure.DependecyInjections;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}