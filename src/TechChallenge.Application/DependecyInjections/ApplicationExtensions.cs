using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Application.Queries;

namespace TechChallenge.Application.DependecyInjections;

public static class ApplicationExtensions
{
    public static void AddCommands(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    public static void AddQueries(this IServiceCollection services)
    {
        services.AddScoped<NewsQueries>();
    }
}